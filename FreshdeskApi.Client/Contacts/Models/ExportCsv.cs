using System.Collections.Generic;
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

        /// <summary>
        /// Error message if an error occurs
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Code error if an error occurs
        /// </summary>
        [JsonProperty("code")]
        public string? CodeError { get; set; }

        /// <summary>
        /// Description of error if certain errors occur e.g. invalid field entered
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Information on errors that have occurred
        /// </summary>
        [JsonProperty("errors")]
        public List<Errors>? Errors { get; set; }
    }

    public class Errors
    {
        /// <summary>
        /// Field entered that was invalid if field failure
        /// </summary>
        [JsonProperty("field")]
        public string? Field { get; set; }

        /// <summary>
        /// Message explaining error
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Code error if an error occurs
        /// </summary>
        [JsonProperty("code")]
        public Errors? Code { get; set; }

    }
}
