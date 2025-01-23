using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TiberHealth.Serializer;

namespace FreshdeskApi.Client.Extensions;

public static class HttpRequestMessageExtensions
{
    public static HttpContent CreateMultipartContent<TBody>(
        this TBody originalRequest
    ) where TBody : class
    {
        var content = FormDataSerializer.Serialize(originalRequest);

        if (
            content is MultipartFormDataContent multipartContent
            && originalRequest is IRequestWithAdditionalMultipartFormDataContent requestWithAdditionalMultipartFormDataContent
        )
        {
            foreach (var additionalHttpContent in requestWithAdditionalMultipartFormDataContent.GetAdditionalMultipartFormDataContent())
            {
                multipartContent.Add(additionalHttpContent.HttpContent, additionalHttpContent.Name);
            }
        }

        return content;
    }

    public static StringContent CreateJsonContent<TBody>(
        this TBody body
    ) where TBody : class => new(
        JsonConvert.SerializeObject(body, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
        Encoding.UTF8,
        "application/json"
    );
}
