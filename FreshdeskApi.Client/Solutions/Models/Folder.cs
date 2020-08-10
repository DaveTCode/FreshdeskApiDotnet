using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618

namespace FreshdeskApi.Client.Solutions.Models
{
    /// <summary>
    ///
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_folder_attributes
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Folder
    {
        /// <summary>
        /// Unique ID of the solution folder
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The category containing the folder.
        ///
        /// NOTE: This may be null depending on which API call generated the
        /// object.
        /// </summary>
        public long? CategoryId { get; set; }

        /// <summary>
        /// Name of the solution folder
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the solution folder
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Accessibility of this folder.
        ///
        /// <seealso cref="FolderVisibility"/>
        /// </summary>
        public FolderVisibility Visibility { get; set; }

        /// <summary>
        /// Solution Folder creation timestamp
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Solution Folder updated timestamp
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }

    public enum FolderVisibility
    {
        AllUsers = 1,
        LoggedInUsers = 2,
        Agents = 3,
        SelectedCompanies = 4,
    }
}
