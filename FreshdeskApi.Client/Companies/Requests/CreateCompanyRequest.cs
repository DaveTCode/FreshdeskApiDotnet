using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Companies.Requests
{
    /// <summary>
    /// Defines the set of properties to create a comapny
    ///
    /// c.f. https://developers.freshdesk.com/api/#create_company
    /// </summary>
    public class CreateCompanyRequest : BaseCompanyRequest
    {
        public CreateCompanyRequest(string name, string[]? domains = null, string? description = null, string? note = null, string? healthScore = null,
            string? accountTier = null, DateTime? renewalDate = null, string? industry = null, Dictionary<string, object?>? customFields = null) : base
            (
                name, domains, description, note, healthScore, accountTier, renewalDate, industry, customFields
            )
        {
        }
    }
}
