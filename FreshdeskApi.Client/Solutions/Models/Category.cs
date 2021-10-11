using System;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Solutions.Models
{
    /// <summary>
    /// Categories broadly classify your solutions page into several sections.
    /// For example, you could place Shipping, Payments and Delivery related
    /// information under the Order Fulfillment category. Another interesting
    /// application of the top level category is when you are providing support
    /// across multiple brands or products.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_category_attributes
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Unique ID of the solution category
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Name of the solution category
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Description of the solution category
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Solution Category creation timestamp
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Solution Category updated timestamp
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// List of portal IDs where this category is visible
        /// </summary>
        [JsonProperty("visible_in_portals")]
        public long[]? VisibleInPortals { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(VisibleInPortals)}: {VisibleInPortals}";
        }
    }
}
