using FreshdeskApi.Client.Solutions.Models;

namespace FreshdeskApi.Client.Solutions.Requests
{
    /// <summary>
    /// Object used to create a new solution folder.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_folder_attributes
    /// </summary>
    public class CreateFolderRequest
    {
        /// <summary>
        /// Name of the solution folder - must be unique across all folders in
        /// the KB.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Description of the solution folder
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// Accessibility of this folder.
        ///
        /// Defaults to <seealso cref="FolderVisibility.AllUsers"/>, sets the v
        /// </summary>
        public FolderVisibility? Visibility { get; }

        /// <summary>
        /// If the folder visibility is
        /// <seealso cref="FolderVisibility.SelectedCompanies"/> then this is set
        /// to the list of company ids that can view the folder.
        /// </summary>
        public long[]? CompanyIds { get; }

        public CreateFolderRequest(string name, string? description = null, FolderVisibility? visibility = null, long[]? companyIds = null)
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
