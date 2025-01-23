using System;
using System.Collections.Generic;

namespace FreshdeskApi.Client.Companies.Requests;

/// <summary>
/// Defines the set of properties to update a comapny
///
/// c.f. https://developers.freshdesk.com/api/#update_company
/// </summary>
public class UpdateCompanyRequest : BaseCompanyRequest
{
    public UpdateCompanyRequest(string? name = null, string[]? domains = null, string? description = null, string? note = null, string? healthScore = null,
        string? accountTier = null, DateTime? renewalDate = null, string? industry = null, Dictionary<string, object?>? customFields = null) : base
    (
        name, domains, description, note, healthScore, accountTier, renewalDate, industry, customFields
    )
    {
    }
}
