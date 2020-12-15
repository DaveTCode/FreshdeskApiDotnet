using System.Net.Http;
using System.Net.Http.Headers;

namespace FreshdeskApi.Client.Tickets.Models
{
    public class FileAttachment
    {
        public string? Name { get; set; }
        public string? MimeType { get; set; }
        public byte[]? FileBytes { get; set; }
    }
}
