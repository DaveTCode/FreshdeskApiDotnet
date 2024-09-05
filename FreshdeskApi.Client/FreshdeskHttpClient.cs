using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using FreshdeskApi.Client.Exceptions;
using FreshdeskApi.Client.Extensions;
using FreshdeskApi.Client.Helpers;
using FreshdeskApi.Client.Infrastructure;
using FreshdeskApi.Client.Models;
using FreshdeskApi.Client.Pagination;
using Newtonsoft.Json;

namespace FreshdeskApi.Client;

public class FreshdeskHttpClient : IFreshdeskHttpClient, IDisposable
{
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
        if (string.IsNullOrWhiteSpace(httpClient.BaseAddress?.AbsoluteUri))
        {
            throw new ArgumentOutOfRangeException(
                nameof(httpClient), httpClient,
                "The http client must have a base uri set"
            );
        }

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
    public static FreshdeskHttpClient Create(
        string freshdeskDomain, string apiKey
    ) => new(new HttpClient().ConfigureHttpClient(new FreshdeskConfiguration
    {
        FreshdeskDomain = freshdeskDomain,
        ApiKey = apiKey,
    }));

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
        string initialUrl,
        IPaginationConfiguration pagingConfiguration,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var questionMarkIndex = initialUrl.IndexOf('?');

        var originalQueryString = string.Empty;

        if (questionMarkIndex >= 0)
        {
            originalQueryString = initialUrl[questionMarkIndex..];
            initialUrl = initialUrl[..questionMarkIndex];
        }

        var queryString = HttpUtility.ParseQueryString(originalQueryString);

        foreach (var parameter in pagingConfiguration.BuildInitialPageParameters())
        {
            queryString.Add(parameter.Key, parameter.Value);
        }

        var uri = UriHelper.CreateUri(initialUrl, queryString);

        using var disposingCollection = new DisposingCollection();

        var page = 1;

        while (uri is not null)
        {
            var pagedResponse = await ExecuteAndParseAsync<T>(
                uri,
                pagingConfiguration,
                disposingCollection,
                cancellationToken
            );

            if (pagingConfiguration.BeforeProcessingPageAsync != null)
            {
                await pagingConfiguration.BeforeProcessingPageAsync(page, uri, cancellationToken)
                    .ConfigureAwait(false);
            }

            foreach (var data in pagedResponse.Items)
            {
                yield return data;
            }

            if (pagingConfiguration.ProcessedPageAsync != null)
            {
                await pagingConfiguration.ProcessedPageAsync(page, uri, cancellationToken)
                    .ConfigureAwait(false);
            }

            // Rebuild the url based on current information
            uri = pagingConfiguration.BuildNextPageUri(page, pagedResponse, initialUrl, originalQueryString);
            page++;

            // ReSharper disable once DisposeOnUsingVariable it is safe to call it repeatably
            disposingCollection.Dispose();
        }
    }

    private async Task<PagedResponse<T>> ExecuteAndParseAsync<T>(
        Uri url,
        IPaginationConfiguration paginationConfiguration,
        DisposingCollection disposingCollection,
        CancellationToken cancellationToken
    )
    {
        var response = await _httpClient
            .GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);

        SetRateLimitValues(response);

        // Handle rate limiting by waiting the specified amount of time
        while (response.StatusCode is HttpStatusCode.TooManyRequests)
        {
            var retryAfterDelta = response.Headers.RetryAfter?.Delta;
            if (retryAfterDelta.HasValue)
            {
                // response reference will be replaced soon
                disposingCollection.Add(response);

                await Task.Delay(retryAfterDelta.Value, cancellationToken);

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
#pragma warning disable CA2000 // Receiver of the FreshdeskApiException is responsible for disposing
                throw new GeneralApiException(response);
#pragma warning restore CA2000
            }
        }

        if (!response.IsSuccessStatusCode)
        {
#pragma warning disable CA2000 // Receiver of the FreshdeskApiException is responsible for disposing
            throw CreateApiException(response);
#pragma warning restore CA2000
        }

        // response will not be used outside of this method (i.e. in Exception)
        disposingCollection.Add(response);

        return await DeserializeResponse<T>(response, paginationConfiguration, cancellationToken);
    }

    private async Task<PagedResponse<T>> DeserializeResponse<T>(
        HttpResponseMessage response,
        IPaginationConfiguration paginationConfiguration,
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        CancellationToken cancellationToken
    )
    {
        await using var contentStream = await response.Content.ReadAsStreamAsync(
#if NET
            cancellationToken
#endif
        );

        using var sr = new StreamReader(contentStream);
#if NET6_0_OR_GREATER
        await
#endif
        using var reader = new JsonTextReader(sr);

        return paginationConfiguration.DeserializeResponse<T>(reader, response.Headers);
    }

    private HttpRequestMessage CreateHttpRequestMessage<TBody>(HttpMethod method, string url, TBody? body)
        where TBody : class
    {
        var httpMessage = new HttpRequestMessage(method, url);

        if (body != null)
        {
            httpMessage.Content = body.IsMultipartFormDataRequired()
                ? body.CreateMultipartContent()
                : body.CreateJsonContent();
        }

        return httpMessage;
    }

    private async Task<HttpResponseMessage> ExecuteRequestAsync<TBody>(
        HttpMethod method, string url, TBody? body, CancellationToken cancellationToken
    )
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

    public async Task<T> ApiOperationAsync<T, TBody>(
        HttpMethod method, string url, TBody? body, CancellationToken cancellationToken
    )
        where T : new()
        where TBody : class
    {
        using var disposingCollection = new DisposingCollection();

        var response = await ExecuteRequestAsync(method, url, body, cancellationToken);

        // Handle rate limiting by waiting the specified amount of time
        while (response.StatusCode == (HttpStatusCode)429)
        {
            var retryAfterDelta = response.Headers.RetryAfter?.Delta;
            if (retryAfterDelta.HasValue)
            {
                // response reference will be replaced soon
                disposingCollection.Add(response);

                await Task.Delay(retryAfterDelta.Value, cancellationToken);

                response = await ExecuteRequestAsync(method, url, body, cancellationToken);
            }
            else
            {
                // Rate limit response received without a time before
                // retry, throw an exception rather than guess a sensible
                // limit
#pragma warning disable CA2000 // Receiver of the FreshdeskApiException is responsible for disposing
                throw new GeneralApiException(response);
#pragma warning restore CA2000
            }
        }

        if (response.IsSuccessStatusCode)
        {
            // response will not be used outside of this method
            disposingCollection.Add(response);

            if (response.StatusCode == HttpStatusCode.NoContent) return new T();

            await using var contentStream = await response.Content.ReadAsStreamAsync(
#if NET
                cancellationToken
#endif
            );
            using var sr = new StreamReader(contentStream);
#if NET6_0_OR_GREATER
            await
#endif
            using var reader = new JsonTextReader(sr);
            var serializer = new JsonSerializer();

            return serializer.Deserialize<T>(reader) ?? throw new ArgumentNullException(
                nameof(serializer.Deserialize),
                "Deserialized response must not be null"
            );
        }

#pragma warning disable CA2000 // Receiver of the FreshdeskApiException is responsible for disposing
        throw CreateApiException(response);
#pragma warning restore CA2000
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
