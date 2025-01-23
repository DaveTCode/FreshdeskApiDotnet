using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Requests;

public class CreateRecordRequest<T>(T data)
{
    [JsonProperty("data")]
    public T Data { get; set; } = data;
}
