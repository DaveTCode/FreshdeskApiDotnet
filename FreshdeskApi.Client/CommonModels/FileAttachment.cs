using FreshdeskApi.Client.Attributes;
using TiberHealth.Serializer.Attributes;

namespace FreshdeskApi.Client.CommonModels;

[IgnoreJsonValidation]
[MultipartFile(ContentType = "MimeType", FileName = "Name", Value = "FileBytes")]
public class FileAttachment
{
    public string? Name { get; set; }

    public string? MimeType { get; set; }

    public byte[]? FileBytes { get; set; }
}
