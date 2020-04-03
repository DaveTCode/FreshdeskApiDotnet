using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Agents;
using FreshdeskApi.Client.Companies;
using FreshdeskApi.Client.Contacts;
using FreshdeskApi.Client.Exceptions;
using FreshdeskApi.Client.Groups;
using FreshdeskApi.Client.Tickets;

[assembly: InternalsVisibleTo("FreshdeskApi.Client.UnitTests")]
namespace FreshdeskApi.Client
{
    public class FreshdeskClient : IDisposable
    {
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

        private readonly HttpClient _httpClient;

        private FreshdeskClient()
        {
            Tickets = new FreshdeskTicketClient(this);
            Contacts = new FreshdeskContactClient(this);
            Groups = new FreshdeskGroupClient(this);
            Agents = new FreshdeskAgentClient(this);
            Companies = new FreshdeskCompaniesClient(this);
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
        public FreshdeskClient(string freshdeskDomain, string apiKey) : this()
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentOutOfRangeException(nameof(apiKey), apiKey, "API Key can't be blank");
            if (string.IsNullOrWhiteSpace(freshdeskDomain)) throw new ArgumentOutOfRangeException(nameof(freshdeskDomain), freshdeskDomain, "Freshdesk domain can't be blank");

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(freshdeskDomain, UriKind.Absolute)
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes($"{apiKey}:X")));
        }

        public FreshdeskClient(HttpClient httpClient) : this()
        {
            _httpClient = httpClient;
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

        internal async IAsyncEnumerable<T> GetPagedResults<T>(string url, [EnumeratorCancellation] CancellationToken cancellationToken = default)
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

                using var contentStream = await response.Content.ReadAsStreamAsync();
                var newData = await JsonSerializer.DeserializeAsync<List<T>>(contentStream, cancellationToken: cancellationToken);

                foreach (var data in newData)
                {
                    yield return data;
                }

                if (!response.Headers.Contains("Location"))
                {
                    morePages = false;
                }
                else
                {
                    url = response.Headers.Location.ToString();
                }
            }
        }

        internal async Task<T> ApiOperationAsync<T>(HttpMethod method, string url, object body = null, CancellationToken cancellationToken = default)
        {
            var httpMessage = new HttpRequestMessage(method, url);

            if (body != null)
            {
                httpMessage.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
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
                if (response.StatusCode == HttpStatusCode.NoContent) return default;

                using var contentStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<T>(contentStream, cancellationToken: cancellationToken);
            }

            throw CreateApiException(response);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
