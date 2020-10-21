using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Models
{
    public class ExportCsv
    {
        /// <summary>
        /// The Id of the export being processed
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// The status of the export being carried out (in progress/finished/etc)
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        /// The URL for the csv contents of the download when the export is complete
        /// </summary>
        [JsonProperty("download_url")]
        public string? DownloadUrl { get; set; }
    }
}
