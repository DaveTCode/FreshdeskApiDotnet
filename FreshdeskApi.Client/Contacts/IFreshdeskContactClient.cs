using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Contacts.Models;
using FreshdeskApi.Client.Contacts.Requests;

namespace FreshdeskApi.Client.Contacts
{
    public interface IFreshdeskContactClient
    {
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
        Task<Contact> ViewContactAsync(
            long contactId,
            CancellationToken cancellationToken = default);

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
        Task<Contact> CreateContactAsync(
            ContactCreateRequest request,
            CancellationToken cancellationToken = default);

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
        IAsyncEnumerable<ListContact> ListAllContactsAsync(
            ListAllContactsRequest request,
            IPaginationConfiguration? pagingConfiguration = null,
            CancellationToken cancellationToken = default);

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
        Task<Contact> UpdateContactAsync(
            long contactId,
            UpdateContactRequest request,
            CancellationToken cancellationToken = default);

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
        Task<Contact> MakeAgentAsync(
            long contactId,
            MakeAgentRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Export contacts with the specified information
        /// </summary>
        ///
        /// <param name="request">
        /// All the details of the fields to be exported.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>A list of exported contacts</returns>
        Task<ExportCsv> StartExportContactsAsync(
            ContactsExportRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if an export has completed
        /// </summary>
        ///
        /// <param name="request">
        /// The Id of the export being checked.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The ExportCsv object which contains an export url to be used</returns>
        Task<ExportCsv> GetExportStatusAsync(
            ExportCsv export,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Merge a primary contact with secondary contacts
        /// </summary>
        ///
        /// <param name="request">
        /// Primary and secondary contacts being merged.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The http response</returns>
        Task MergeContactsAsync(
            MergeContactsRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft Delete a Contact.
        ///
        /// c.f. https://developers.freshdesk.com/api/#delete_contact
        /// </summary>
        ///
        /// <param name="contactId">
        /// The contact to delete.
        /// </param>
        ///
        ///
        /// <param name="cancellationToken"></param>
        Task DeleteContactAsync(
            long contactId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Permanently Delete a Contact<br />
        /// Hard delete a contact to completely remove it from the portal. Can be used for GDPR compliance.
        ///
        /// c.f. https://developers.freshdesk.com/api/#hard_delete_contact
        /// </summary>
        ///
        /// <param name="contactId">
        /// The contact to delete.
        /// </param>
        /// 
        /// <param name="force">
        /// Send as true to force hard delete of a contact that is not already soft deleted
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        Task PermanentlyDeleteContactAsync(
            long contactId,
            bool force,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Used to send an activation email to an existing contact for email verification.
        ///
        /// c.f. https://developers.freshdesk.com/api/#send_invite
        /// </summary>
        /// <param name="contactId">
        /// The contact to invite
        /// </param>
        /// 
        /// <param name="cancellationToken"></param>
        Task SendInviteAsync(
            long contactId,
            CancellationToken cancellationToken = default);
    }
}
