using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("FreshdeskApi.Client.UnitTests")]
namespace FreshdeskApi.Client
{
    public class FreshdeskClient : IDisposable
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

        public FreshdeskTicketClient Tickets { get; }

        public FreshdeskContactClient Contacts { get; }

        public FreshdeskGroupClient Groups { get; }

        public FreshdeskAgentClient Agents { get; }

        public FreshdeskCompaniesClient Companies { get; }

        public FreshdeskSolutionClient Solutions { get; }

        public TicketFieldsClient TicketFields { get; }

        public ConversationsClient Conversations { get; }

        public ChannelApiClient ChannelApi { get; }

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
                _ => throw new GeneralApiException(response)
            };
        }

        internal async IAsyncEnumerable<T> GetPagedResults<T>(string url, bool newStylePages, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (url.Contains("?")) url += "&page=1";
            else url += "?page=1";

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

                foreach (var data in newData ?? new List<T>())
                {
                    yield return data;
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
                    }
                }
                else
                {
                    morePages = false;
                }
            }
        }

        internal async Task<T> ApiOperationAsync<T>(HttpMethod method, string url, object? body = null, CancellationToken cancellationToken = default)
        {
            var httpMessage = new HttpRequestMessage(method, url);

            if (body != null)
            {
                httpMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(body, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    Encoding.UTF8,
                    "application/json");
            }

            var response = await _httpClient
                .SendAsync(httpMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);
            SetRateLimitValues(response);

            // Handle rate limiting by waiting the specified amount of time
            while (response.StatusCode == (HttpStatusCode)429)
            {
                if (response.Headers.RetryAfter.Delta.HasValue)
                {
                    await Task.Delay(response.Headers.RetryAfter.Delta.Value, cancellationToken);
                    response = await _httpClient
                        .SendAsync(httpMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
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

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent) return default!;

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
