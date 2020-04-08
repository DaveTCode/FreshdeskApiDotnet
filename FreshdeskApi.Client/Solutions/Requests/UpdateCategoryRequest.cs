namespace FreshdeskApi.Client.Solutions.Requests
{
    /// <summary>
    /// Contains the information required to update a category
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_category_attributes
    /// </summary>
    public class UpdateCategoryRequest
    {
        /// <summary>
        /// Name of the solution category
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Description of the solution category
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// List of portal IDs where this category is visible.
        ///
        /// Allowed only if the account is configured with multiple portals.
        /// </summary>
        public long[] VisibleInPortals { get; }

        public UpdateCategoryRequest(string name = null, string description = null, long[] visibleInPortals = null)
        {
            Name = name;
            Description = description;
            VisibleInPortals = visibleInPortals;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(VisibleInPortals)}: {VisibleInPortals}";
        }
    }
}
