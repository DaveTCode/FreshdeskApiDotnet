using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Contacts.Models;
using FreshdeskApi.Client.Contacts.Requests;

namespace FreshdeskApi.Client.Contacts
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FreshdeskContactClient : IFreshdeskContactClient
    {
        private readonly FreshdeskClient _freshdeskClient;

        public FreshdeskContactClient(FreshdeskClient freshdeskClient)
        {
            _freshdeskClient = freshdeskClient;
        }

        /// <summary>
        /// Retrieve all details about a single contact by their id.
        ///
        /// c.f. https://developers.freshdesk.com/api/#view_contact
        /// </summary>
        /// <param name="contactId">
        /// The unique identifier for the contact.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The full contact information</returns>
        public async Task<Contact> ViewContactAsync(
            long contactId,
            CancellationToken cancellationToken = default)
        {
            return await _freshdeskClient
                .ApiOperationAsync<Contact>(HttpMethod.Get, $"/api/v2/contacts/{contactId}", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new contact with the specified information
        /// </summary>
        ///
        /// <param name="request">
        /// All the details of the contact to be created.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The newly created contact</returns>
        public async Task<Contact> CreateContactAsync(
            ContactCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _freshdeskClient
                .ApiOperationAsync<Contact>(HttpMethod.Post, "/api/v2/contacts", request, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Filter the list of contacts according to a set of predefined filters.
        ///
        /// c.f. https://developers.freshdesk.com/api/#list_all_contacts
        /// </summary>
        /// 
        /// <param name="request">
        /// A <seealso cref="ListAllContactsRequest"/> object which contains
        /// the filters that we want to apply. By default will include all
        /// unblocked/undeleted contacts.
        /// </param>
        ///
        /// <param name="pagingConfiguration"></param>
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// The full set of contacts matching the filters supplied, this
        /// request is paged and iterating to the next entry may cause a new
        /// API call to get the next page.
        /// </returns>
        public async IAsyncEnumerable<ListContact> ListAllContactsAsync(
            ListAllContactsRequest request,
            PaginationConfiguration? pagingConfiguration = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            await foreach (var contact in _freshdeskClient.GetPagedResults<ListContact>(request.UrlWithQueryString, pagingConfiguration, false, cancellationToken).ConfigureAwait(false))
            {
                yield return contact;
            }
        }

        /// <summary>
        /// Update a contact with new details.
        ///
        /// c.f. https://developers.freshdesk.com/api/#update_contact
        /// </summary>
        /// 
        /// <param name="contactId">
        /// The unique identifier for the contact.
        /// </param>
        ///
        /// <param name="request">
        /// The details about the contact to update.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The newly updated contact</returns>
        public async Task<Contact> UpdateContactAsync(
            long contactId,
            UpdateContactRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _freshdeskClient
                .ApiOperationAsync<Contact>(HttpMethod.Put, $"/api/v2/contacts/{contactId}", request, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Convert a contact into an agent
        ///
        /// Note:
        /// 1. The contact must have an email address in order to be converted
        ///    into an agent
        /// 2. If the contact has 'other_emails' they will be deleted after
        ///    the conversion
        /// 3. If your account has already reached the maximum number of
        ///    agents, the API request will fail with HTTP error code 403
        /// 4. The agent whose credentials (identified by the API key) are
        ///    used to make the API call should be authorised to convert a
        ///    contact into an agent.
        ///
        /// c.f. https://developers.freshdesk.com/api/#make_agent
        /// </summary>
        /// 
        /// <param name="contactId">
        /// The unique contact identifier.
        /// </param>
        /// 
        /// <param name="request">
        /// Specify what agent specific information to set.
        ///
        /// e.g. Agent is fulltime or occasional
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns></returns>
        public async Task<Contact> MakeAgentAsync(
            long contactId,
            MakeAgentRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _freshdeskClient
                .ApiOperationAsync<Contact>(HttpMethod.Put, $"/api/v2/contacts/{contactId}/make_agent", request, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
