using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Exceptions;
using FreshdeskApi.Client.Extensions;
using FreshdeskApi.Client.Infrastructure;
using Newtonsoft.Json;
using TiberHealth.Serializer;

namespace FreshdeskApi.Client
{
    public class FreshdeskHttpClient : IFreshdeskHttpClient, IDisposable
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
        public FreshdeskHttpClient(HttpClient httpClient)
        {
            if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));
            if (string.IsNullOrWhiteSpace(httpClient.BaseAddress?.AbsoluteUri)) throw new ArgumentOutOfRangeException(nameof(httpClient), httpClient, "The http client must have a base uri set");

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
        public FreshdeskHttpClient(
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

        public async IAsyncEnumerable<T> GetPagedResults<T>(
            string url,
            IPaginationConfiguration? pagingConfiguration,
            bool newStylePages,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            pagingConfiguration ??= new PaginationConfiguration();

            var page = pagingConfiguration.StartingPage;
            if (url.Contains("?")) url += $"&page={page}";
            else url += $"?page={page}";

            if (pagingConfiguration.PageSize.HasValue)
            {
                url += $"&per_page={pagingConfiguration.PageSize}";
            }

            using var disposingCollection = new DisposingCollection();

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
                        // response reference will be replaced soon
                        disposingCollection.Add(response);

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

                // response will not be used outside of this method (i.e. in Exception)
                disposingCollection.Add(response);

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

                // it is safe to call it repeatably
                disposingCollection.Dispose();
            }
        }

        private HttpRequestMessage CreateHttpRequestMessage<TBody>(HttpMethod method, string url, TBody? body)
            where TBody : class
        {
            var httpMessage = new HttpRequestMessage(method, url);

            if (body != null)
            {
                httpMessage.Content = body.IsMultipartFormDataRequired()
                    ? FormDataSerializer.Serialize(body)
                    : new StringContent(
                        JsonConvert.SerializeObject(body, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                        Encoding.UTF8,
                        "application/json"
                    );
            }

            return httpMessage;
        }

        private async Task<HttpResponseMessage> ExecuteRequestAsync<TBody>(HttpMethod method, string url, TBody? body, CancellationToken cancellationToken)
            where TBody : class
        {
            using var httpMessage = CreateHttpRequestMessage(method, url, body);

            var response = await _httpClient
                .SendAsync(httpMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);

            SetRateLimitValues(response);

            return response;
        }

        public Task<T> ApiOperationAsync<T>(HttpMethod method, string url, CancellationToken cancellationToken)
            where T : new() => ApiOperationAsync<T, object>(method, url, null, cancellationToken);

        public async Task<T> ApiOperationAsync<T, TBody>(HttpMethod method, string url, TBody? body, CancellationToken cancellationToken)
            where T : new()
            where TBody : class
        {
            using var disposingCollection = new DisposingCollection();

            var response = await ExecuteRequestAsync(method, url, body, cancellationToken);

            // Handle rate limiting by waiting the specified amount of time
            while (response.StatusCode == (HttpStatusCode)429)
            {
                if (response.Headers.RetryAfter.Delta.HasValue)
                {
                    // response reference will be replaced soon
                    disposingCollection.Add(response);

                    await Task.Delay(response.Headers.RetryAfter.Delta.Value, cancellationToken);

                    response = await ExecuteRequestAsync(method, url, body, cancellationToken);
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
                // response will not be used outside of this method
                disposingCollection.Add(response);

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
            _httpClient.Dispose();
        }
    }
}
