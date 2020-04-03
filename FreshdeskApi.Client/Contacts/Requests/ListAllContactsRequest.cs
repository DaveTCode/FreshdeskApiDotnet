using System;
using System.Collections.Generic;
using System.Linq;
using FreshdeskApi.Client.Contacts.Models;

namespace FreshdeskApi.Client.Contacts.Requests
{
    public class ListAllContactsRequest
    {
        private const string ListAllContactsUrl = "/api/v2/contacts";

        private readonly string _email;
        private readonly string _mobile;
        private readonly string _phone;
        private readonly long? _companyId;
        private readonly ContactState? _contactState;
        private readonly DateTimeOffset? _updatedSince;

        public ListAllContactsRequest(
            string email = null,
            string mobile = null,
            string phone = null,
            long? companyId = null,
            ContactState? contactState = null,
            DateTimeOffset? updatedSince = null)
        {
            _email = email;
            _mobile = mobile;
            _phone = phone;
            _companyId = companyId;
            _contactState = contactState;
            _updatedSince = updatedSince;
        }

        internal string GetUrl()
        {
            var urlParams = new List<string>
            {
                (_email != null) ? $"email={_email}" : null,
                (_mobile != null) ? $"mobile={_mobile}" : null,
                (_phone != null) ? $"phone={_phone}" : null,
                (_companyId != null) ? $"company_id={_companyId}" : null,
                (_contactState != null) ? $"state={_contactState.Value.GetQueryStringValue()}" : null,
                (_updatedSince != null) ? $"updated_since={_updatedSince:yyyy-MM-ddTHH:mm:ssZ}" : null,
            }.Select(x => x != null).ToList();

            return ListAllContactsUrl +
                   (urlParams.Any() ? "?" + string.Join("&", urlParams) : "");
        }
    }
}
