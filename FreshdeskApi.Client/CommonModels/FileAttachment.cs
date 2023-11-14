using TiberHealth.Serializer.Attributes;

namespace FreshdeskApi.Client.CommonModels;

[MultipartFile(ContentType = "MimeType", FileName = "Name", Value = "FileBytes")]
public class FileAttachment
{
    public string? Name { get; set; }

    public string? MimeType { get; set; }

    public byte[]? FileBytes { get; set; }
}
