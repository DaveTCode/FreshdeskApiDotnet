using FreshdeskApi.Client.Solutions.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Solutions.Requests
{
    /// <summary>
    /// Object used to update a solution folder.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_folder_attributes
    /// </summary>
    public class UpdateFolderRequest
    {
        /// <summary>
        /// Name of the solution folder - must be unique across all folders in
        /// the KB.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; }

        /// <summary>
        /// Description of the solution folder
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; }

        /// <summary>
        /// Accessibility of this folder.
        ///
        /// Defaults to <seealso cref="FolderVisibility.AllUsers"/>, sets the v
        /// </summary>
        [JsonProperty("visibility")]
        public FolderVisibility? Visibility { get; }

        /// <summary>
        /// If the folder visibility is
        /// <seealso cref="FolderVisibility.SelectedCompanies"/> then this is set
        /// to the list of company ids that can view the folder.
        /// </summary>
        [JsonProperty("company_ids")]
        public long[]? CompanyIds { get; }

        public UpdateFolderRequest(string? name = null, string? description = null, FolderVisibility? visibility = null, long[]? companyIds = null)
        {
            Name = name;
            Description = description;
            Visibility = visibility;
            CompanyIds = companyIds;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Visibility)}: {Visibility}, {nameof(CompanyIds)}: {CompanyIds}";
        }
    }
}
