using TiberHealth.Serializer.Attributes;

// TODO move to namespace with BC-break release
namespace FreshdeskApi.Client
{
    [MultipartFile(ContentType = "MimeType", FileName = "Name", Value = "FileBytes")]
    public class FileAttachment
    {
        public string? Name { get; set; }

        public string? MimeType { get; set; }

        public byte[]? FileBytes { get; set; }
    }
}
