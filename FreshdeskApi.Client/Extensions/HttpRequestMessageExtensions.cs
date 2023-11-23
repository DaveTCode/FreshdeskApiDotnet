using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using FreshdeskApi.Client.Tickets.Requests;
using Newtonsoft.Json;
using TiberHealth.Serializer;

namespace FreshdeskApi.Client.Extensions;

public static class HttpRequestMessageExtensions
{
    public static HttpContent CreateMultipartContent(this CreateTicketRequest originalRequest)
    {
        var newRequest = originalRequest.MapToWithoutCustomFields();
        var content = FormDataSerializer.Serialize(newRequest);

        content.AddCustomFields(originalRequest.CustomFields);
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
