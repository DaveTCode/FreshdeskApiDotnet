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

    public static CreateTicketRequestWithoutCustomFields MapToWithoutCustomFields(this CreateTicketRequest originalRequest)
    {
        var newRequest = new CreateTicketRequestWithoutCustomFields(
             originalRequest.Status,
             originalRequest.Priority,
             originalRequest.Source,
             originalRequest.Description,
             originalRequest.RequesterName,
             originalRequest.RequesterId,
             originalRequest.Email,
             originalRequest.FacebookId,
             originalRequest.PhoneNumber,
             originalRequest.TwitterId,
             originalRequest.UniqueExternalId,
             originalRequest.ResponderId,
             originalRequest.CcEmails,
             originalRequest.DueBy,
             originalRequest.EmailConfigId,
             originalRequest.FirstResponseDueBy,
             originalRequest.GroupId,
             originalRequest.ProductId,
             originalRequest.Tags,
             originalRequest.CompanyId,
             originalRequest.Subject,
             originalRequest.TicketType,
             originalRequest.ParentTicketId,
             originalRequest.Files);

        return newRequest;
    }

    public static void AddCustomFields(this HttpContent content, Dictionary<string, object>? customFields)
    {
        if (content is MultipartFormDataContent multipartContent && customFields?.Any() == true)
        {
            foreach (var customField in customFields)
            {
                var key = $"custom_fields[{customField.Key}]";
                var value = customField.Value?.ToString() ?? string.Empty;

#pragma warning disable CA2000
                multipartContent.Add(new StringContent(value), key);
#pragma warning restore CA2000
            }
        }
    }

    public static StringContent CreateJsonContent<TBody>(
        this TBody body
    ) where TBody : class => new(
        JsonConvert.SerializeObject(body, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
        Encoding.UTF8,
        "application/json"
    );
}
