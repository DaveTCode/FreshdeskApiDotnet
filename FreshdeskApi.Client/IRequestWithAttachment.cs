namespace FreshdeskApi.Client;

public interface IRequestWithAttachment
{
    /// <summary>
    /// Determines if the request should be serialized as a MultiPart Form Data
    /// </summary>
    /// <returns>Boolean indicating if the body should be serialized as MultiPart Form Data.</returns>
    bool IsMultipartFormDataRequired();
}
