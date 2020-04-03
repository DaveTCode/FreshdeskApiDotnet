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
    public class FreshdeskContactClient
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
            return await _freshdeskClient.ApiOperationAsync<Contact>(HttpMethod.Get, $"/api/v2/contacts/{contactId}", cancellationToken: cancellationToken);
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
            return await _freshdeskClient.ApiOperationAsync<Contact>(HttpMethod.Post, "/api/v2/contacts", request, cancellationToken);
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
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// The full set of contacts matching the filters supplied, this
        /// request is paged and iterating to the next entry may cause a new
        /// API call to get the next page.
        /// </returns>
        public async IAsyncEnumerable<Contact> ListAllContactsAsync(
            ListAllContactsRequest request,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var contact in _freshdeskClient.GetPagedResults<Contact>(request.GetUrl(), cancellationToken))
            {
                yield return contact;
            }
        }
    }
}
