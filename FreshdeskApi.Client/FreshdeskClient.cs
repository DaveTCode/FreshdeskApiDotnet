using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Agents;
using FreshdeskApi.Client.Channel;
using FreshdeskApi.Client.Companies;
using FreshdeskApi.Client.Contacts;
using FreshdeskApi.Client.Conversations;
using FreshdeskApi.Client.Exceptions;
using FreshdeskApi.Client.Groups;
using FreshdeskApi.Client.Solutions;
using FreshdeskApi.Client.TicketFields;
using FreshdeskApi.Client.Tickets;
using FreshdeskApi.Client.Tickets.Models;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("FreshdeskApi.Client.UnitTests")]
namespace FreshdeskApi.Client
{
    public class FreshdeskClient : IFreshdeskClient, IDisposable
    {
        /// <summary>
        /// Note this is obviously not a full method for parsing RFC5988 link
        /// headers. I don't currently believe one is needed for the Freshdesk
        /// API.
        /// </summary>
        private static readonly Regex LinkHeaderRegex = new Regex(@"\<(?<url>.+)?\>");

        /// <summary>
        /// The rate limit remaining after the last request was completed.
        ///
        /// Will be -1 until a request has been made.
        /// </summary>
        public long RateLimitRemaining { get; private set; } = -1;

        public long RateLimitTotal { get; private set; } = -1;

        public IFreshdeskTicketClient Tickets { get; }

        public IFreshdeskContactClient Contacts { get; }

        public IFreshdeskGroupClient Groups { get; }

        public IFreshdeskAgentClient Agents { get; }

        public IFreshdeskCompaniesClient Companies { get; }

        public IFreshdeskSolutionClient Solutions { get; }

        public ITicketFieldsClient TicketFields { get; }

        public IConversationsClient Conversations { get; }

        public IChannelApiClient ChannelApi { get; }

        private readonly HttpClient _httpClient;

        /// <summary>
        /// Construct a freshdesk client object when you already have access
        /// to HttpClient objects or want to otherwise pool them outside of
        /// this client.
        ///
        /// It is recommended that you use this method in long lived
        /// applications where many of these clients will be created.
        /// </summary>
        /// <param name="httpClient">
        /// A HttpClient object with authentication and
        /// <seealso cref="HttpClient.BaseAddress"/> already set.
        /// </param>
        public FreshdeskClient(HttpClient httpClient)
        {
            if (string.IsNullOrWhiteSpace(httpClient?.BaseAddress?.AbsoluteUri)) throw new ArgumentOutOfRangeException(nameof(httpClient), httpClient, "The http client must have a base uri set");

            Tickets = new FreshdeskTicketClient(this);
            Contacts = new FreshdeskContactClient(this);
            Groups = new FreshdeskGroupClient(this);
            Agents = new FreshdeskAgentClient(this);
            Companies = new FreshdeskCompaniesClient(this);
            Solutions = new FreshdeskSolutionClient(this);
            TicketFields = new TicketFieldsClient(this);
            Conversations = new ConversationsClient(this);
            ChannelApi = new ChannelApiClient(this);

            _httpClient = httpClient;
        }

        /// <summary>
        /// Construct a freshdesk client object from just domain and api key.
        ///
        /// This object wil encapsulate a single <see cref="HttpClient"/>
        /// object and is therefore disposable. All of the normal issues with
        /// HttpClient are carried over.
        /// </summary>
        /// 
        /// <param name="freshdeskDomain">
        /// The full domain on which your Freshdesk account is hosted. e.g.
        /// https://yourdomain.freshdesk.com. This must include the
        /// scheme (http/https)
        /// </param>
        ///
        /// <param name="apiKey">
        /// The API key from freshdesk of a user with sufficient permissions to
        /// perform whichever operations you are calling.
        /// </param>
        // ReSharper disable once UnusedMember.Global
        public FreshdeskClient(
            string freshdeskDomain, string apiKey
        ) : this(new HttpClient().ConfigureFreshdeskApi(freshdeskDomain, apiKey))
        {
        }

        private void SetRateLimitValues(HttpResponseMessage response)
        {
            if (response.Headers.TryGetValues("X-RateLimit-Total", out var rateLimitTotalValues))
            {
                var rateLimitTotalValuesArray = rateLimitTotalValues.ToArray();
                if (rateLimitTotalValuesArray.Length == 1)
                {
                    if (long.TryParse(rateLimitTotalValuesArray[0], out var rateLimitTotal))
                    {
                        RateLimitTotal = rateLimitTotal;
                    }
                }
            }

            if (response.Headers.TryGetValues("X-RateLimit-Remaining", out var rateLimitRemainingValues))
            {
                var rateLimitRemainingValuesArray = rateLimitRemainingValues.ToArray();
                if (rateLimitRemainingValuesArray.Length == 1)
                {
                    if (long.TryParse(rateLimitRemainingValuesArray[0], out var rateLimitRemaining))
                    {
                        RateLimitRemaining = rateLimitRemaining;
                    }
                }
            }
        }

        private static FreshdeskApiException CreateApiException(HttpResponseMessage response)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return response.StatusCode switch
            {
                HttpStatusCode.BadRequest => new InvalidFreshdeskRequest(response), // 400
                HttpStatusCode.Unauthorized => new AuthenticationFailureException(response), // 400
                HttpStatusCode.Forbidden => new AuthorizationFailureException(response), // 400
                HttpStatusCode.NotFound => new ResourceNotFoundException(response), // 400
                HttpStatusCode.Conflict => new ResourceConflictException(response), // 400
                _ => throw new GeneralApiException(response),
            };
        }

        internal async IAsyncEnumerable<T> GetPagedResults<T>(
            string url,
            IPaginationConfiguration? pagingConfiguration,
            bool newStylePages,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            pagingConfiguration ??= new PaginationConfiguration();

            var page = pagingConfiguration.StartingPage;
            if (url.Contains("?")) url += $"&page={page}";
            else url += $"?page={page}";

            if (pagingConfiguration.PageSize.HasValue)
            {
                url += $"&per_page={pagingConfiguration.PageSize}";
            }

            var morePages = true;

            while (morePages)
            {
                var response = await _httpClient
                    .GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                SetRateLimitValues(response);

                // Handle rate limiting by waiting the specified amount of time
                while (response.StatusCode == (HttpStatusCode)429)
                {
                    if (response.Headers.RetryAfter.Delta.HasValue)
                    {
                        await Task.Delay(response.Headers.RetryAfter.Delta.Value, cancellationToken);
                        response = await _httpClient
                            .GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                            .ConfigureAwait(false);
                        SetRateLimitValues(response);
                    }
                    else
                    {
                        // Rate limit response received without a time before
                        // retry, throw an exception rather than guess a sensible
                        // limit
                        throw new GeneralApiException(response);
                    }
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw CreateApiException(response);
                }

                await using var contentStream = await response.Content.ReadAsStreamAsync();
                using var sr = new StreamReader(contentStream);
                using var reader = new JsonTextReader(sr);
                var serializer = new JsonSerializer();

                var newData = newStylePages
                    ? serializer.Deserialize<PagedResult<T>>(reader)?.Results
                    : serializer.Deserialize<List<T>>(reader);

                if (pagingConfiguration.BeforeProcessingPageAsync != null)
                {
                    await pagingConfiguration.BeforeProcessingPageAsync(page, cancellationToken).ConfigureAwait(false);
                }

                foreach (var data in newData ?? new List<T>())
                {
                    yield return data;
                }

                if (pagingConfiguration.ProcessedPageAsync != null)
                {
                    await pagingConfiguration.ProcessedPageAsync(page, cancellationToken).ConfigureAwait(false);
                }

                // Handle a link header reflecting that there's another page of data
                if (response.Headers.TryGetValues("link", out var linkHeaderValues))
                {
                    var linkHeaderValue = linkHeaderValues.FirstOrDefault();
                    if (linkHeaderValue == null || !LinkHeaderRegex.IsMatch(linkHeaderValue))
                    {
                        morePages = false;
                    }
                    else
                    {
                        var nextLinkMatch = LinkHeaderRegex.Match(linkHeaderValue);
                        url = nextLinkMatch.Groups["url"].Value;
                        page++;
                    }
                }
                else if (newStylePages)
                {
                    // only returns 10 pages of data maximum because for some api calls, e.g. for getting filtered tickets,
                    // To scroll through the pages you add page parameter to the url. The page number starts with 1 and should not exceed 10.
                    // as can be seen here: https://developers.freshdesk.com/api/#filter_tickets
                    if (newData != null && newData.Any() && page < 10 && url.Contains("page"))
                    {
                        url = url.Replace($"page={page}", $"page={page + 1}");
                        page++;
                    }
                    else
                    {
                        morePages = false;
                    }
                }
                else
                {
                    morePages = false;
                }
            }
        }

        private HttpRequestMessage CreateHttpRequestMessage<T>(HttpMethod method, string url, T? body, IEnumerable<FileAttachment>? files)
            where T : class
        {

            var httpMessage = new HttpRequestMessage(method, url);

            if (body != null)
            {
                httpMessage.Content = (files != null && files.Count() > 0) ?
                    GetMultipartContent(body, files) :
                    new StringContent(
                    JsonConvert.SerializeObject(body, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    Encoding.UTF8,
                    "application/json"
                );
            }

            return httpMessage;
        }

        private HttpContent GetMultipartContent<T>(T? body, IEnumerable<FileAttachment>? files)
            where T : class
        {
            var form = new MultipartFormDataContent();
            if (body == null) return form;

            foreach (var property in typeof(T).GetProperties() ?? Array.Empty<PropertyInfo>())
            {
                if (property.GetCustomAttribute<JsonIgnoreAttribute>() != null) continue;

                var value = property.GetValue(body);
                if (value != null)
                {
                    var propertyValue = property.PropertyType.IsEnum ?
                        ((int)value).ToString() :
                        value.ToString();

                    var jProperty = property.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName;
                    var stringContent = new StringContent(propertyValue);
                    stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = $"\"{jProperty ?? property.Name}\""
                    };
                    form.Add(stringContent);
                }
            }

            // Attach files, if any
            foreach (var file in files ?? Array.Empty<FileAttachment>())
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var content = new ByteArrayContent(file.FileBytes, 0, file.FileBytes.Length);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    FileName = file.Name,
                    Name = "attachments[]"
                };
                form.Add(content, "attachments[]");
            }

            return form;
        }

        private async Task<HttpResponseMessage> ExecuteRequestAsync<T>(HttpMethod method, string url, T? body, CancellationToken cancellationToken, IEnumerable<FileAttachment>? files)
            where T : class
        {
            var httpMessage = CreateHttpRequestMessage(method, url, body, files);

            var response = await _httpClient
                .SendAsync(httpMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);

            SetRateLimitValues(response);

            return response;
        }

        internal Task<T> ApiOperationAsync<T>(HttpMethod method, string url, object? body = null, CancellationToken cancellationToken = default)
            where T : new() => ApiOperationAsync<T, object>(method, url, body, cancellationToken, null);

        internal async Task<T> ApiOperationAsync<T, R>(HttpMethod method, string url, R? body, CancellationToken cancellationToken = default, IEnumerable<FileAttachment>? files = null)
            where T : new()
            where R : class
        {
            var response = await ExecuteRequestAsync(method, url, body, cancellationToken, files);

            // Handle rate limiting by waiting the specified amount of time
            while (response.StatusCode == (HttpStatusCode)429)
            {
                if (response.Headers.RetryAfter.Delta.HasValue)
                {
                    await Task.Delay(response.Headers.RetryAfter.Delta.Value, cancellationToken);

                    response = await ExecuteRequestAsync(method, url, body, cancellationToken, files);
                }
                else
                {
                    // Rate limit response received without a time before
                    // retry, throw an exception rather than guess a sensible
                    // limit
                    throw new GeneralApiException(response);
                }
            }

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent) return new T();

                await using var contentStream = await response.Content.ReadAsStreamAsync();
                using var sr = new StreamReader(contentStream);
                using var reader = new JsonTextReader(sr);
                var serializer = new JsonSerializer();

                return serializer.Deserialize<T>(reader) ?? throw new ArgumentNullException(nameof(serializer.Deserialize), "Deserialized response must not be null");
            }

            throw CreateApiException(response);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
