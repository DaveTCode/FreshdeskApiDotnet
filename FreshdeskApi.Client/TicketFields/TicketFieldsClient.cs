using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.TicketFields.Models;
using FreshdeskApi.Client.TicketFields.Requests;

namespace FreshdeskApi.Client.TicketFields
{
    /// <summary>
    /// Covers API calls referring to the ticket field configuration.
    ///
    /// Note that only admin users can access these API calls.
    ///
    /// c.f. https://developers.freshdesk.com/api/#ticket-fields
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class TicketFieldsClient : ITicketFieldsClient
    {
        private readonly FreshdeskClient _freshdeskClient;

        public TicketFieldsClient(FreshdeskClient freshdeskClient)
        {
            _freshdeskClient = freshdeskClient;
        }

        /// <summary>
        /// Return the full list of ticket fields
        ///
        /// c.f. https://developers.freshdesk.com/api/#list_all_ticket_fields
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// A list of all configured ticket fields, note that this API is not
        /// paged so an entire list is returned every time.
        /// </returns>
        public async Task<List<TicketField>> ListAllTicketFieldsAsync(
            CancellationToken cancellationToken = default)
        {
            return await _freshdeskClient
                .ApiOperationAsync<List<TicketField>>(HttpMethod.Get, "/api/v2/ticket_fields", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Return a single ticket field by it's id
        ///
        /// c.f. https://developers.freshdesk.com/api/#view_ticket_field
        /// </summary>
        /// 
        /// <param name="ticketFieldId">
        /// The unique field identifier
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The complete ticket field information</returns>
        public async Task<TicketField> ViewTicketFieldAsync(
            long ticketFieldId,
            CancellationToken cancellationToken = default)
        {
            return await _freshdeskClient
                .ApiOperationAsync<TicketField>(HttpMethod.Get, $"/api/v2/admin/ticket_fields/{ticketFieldId}", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a ticket field
        ///
        /// c.f. https://developers.freshdesk.com/api/#delete_ticket_field
        /// </summary>
        /// 
        /// <param name="ticketFieldId">
        /// The unique field identifier
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        public async Task DeleteTicketFieldAsync(
            long ticketFieldId,
            CancellationToken cancellationToken = default)
        {
            await _freshdeskClient
                .ApiOperationAsync<object>(HttpMethod.Delete, $"/api/v2/admin/ticket_fields/{ticketFieldId}", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new ticket field
        ///
        /// c.f. https://developers.freshdesk.com/api/#create_ticket_field
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TicketField> CreateTicketFieldAsync(
            CreateTicketFieldRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _freshdeskClient
                .ApiOperationAsync<TicketField>(HttpMethod.Post, "/api/v2/admin/ticket_fields", request, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Update a ticket field with new properties.
        ///
        /// c.f. https://developers.freshdesk.com/api/#update_ticket_field
        /// </summary>
        /// 
        /// <param name="ticketFieldId">
        /// The unique identifier of the ticket field to update.
        /// </param>
        ///
        /// <param name="request">
        /// An object encapsulating the properties to update.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The newly updated ticket field</returns>
        public async Task<TicketField> UpdateTicketFieldAsync(
            long ticketFieldId,
            UpdateTicketFieldRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _freshdeskClient
                .ApiOperationAsync<TicketField>(HttpMethod.Put, $"/api/v2/admin/ticket_fields/{ticketFieldId}", request, cancellationToken)
                .ConfigureAwait(false);
        }

        #region Sections

        /// <summary>
        /// Returns the set of sections within a ticket field
        ///
        /// c.f. https://developers.freshdesk.com/api/#list_all_section_fields
        /// </summary>
        /// 
        /// <param name="ticketFieldId">
        /// The unique identifier for the parent ticket field.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// All the sections in a single list (since the API is not
        /// paged)
        /// </returns>
        public async Task<List<Section>> ListAllSectionsAsync(
            long ticketFieldId,
            CancellationToken cancellationToken = default)
        {
            return await _freshdeskClient
                .ApiOperationAsync<List<Section>>(HttpMethod.Get, $"/api/v2/admin/ticket_fields/{ticketFieldId}/sections", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Get all details about a specific section.
        ///
        /// c.f. https://developers.freshdesk.com/api/#list_specific_section
        /// </summary>
        /// 
        /// <param name="ticketFieldId">
        /// The unique ticket field identifier
        /// </param>
        ///
        /// <param name="sectionId">
        /// The unique section identifier
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// All details about the section
        /// </returns>
        public async Task<Section> ViewSectionAsync(
            long ticketFieldId,
            long sectionId,
            CancellationToken cancellationToken = default)
        {
            return await _freshdeskClient
                .ApiOperationAsync<Section>(HttpMethod.Get, $"/api/v2/admin/ticket_fields/{ticketFieldId}/sections/{sectionId}", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a section
        ///
        /// Note: Be cautious about deleting a Section since this action is
        /// irreversible.
        /// You will not be able to restore a Section if you delete it.
        ///
        /// c.f. https://developers.freshdesk.com/api/#delete_section
        /// </summary>
        /// <param name="ticketFieldId"></param>
        /// <param name="sectionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DeleteSectionAsync(
            long ticketFieldId,
            long sectionId,
            CancellationToken cancellationToken = default)
        {
            await _freshdeskClient
                .ApiOperationAsync<object>(HttpMethod.Delete, $"/api/v2/admin/ticket_fields/{ticketFieldId}/sections/{sectionId}", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new section within a ticket field.
        ///
        /// c.f. https://developers.freshdesk.com/api/#create_section
        /// </summary>
        /// 
        /// <param name="ticketFieldId">
        /// The unique identifier for the ticket field
        /// </param>
        ///
        /// <param name="request">
        /// The request encapsulating the properties to set on the new section.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The newly created section</returns>
        public async Task<Section> CreateSectionAsync(
            long ticketFieldId,
            CreateSectionRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _freshdeskClient
                .ApiOperationAsync<Section>(HttpMethod.Post, $"/api/v2/admin/ticket_fields/{ticketFieldId}/sections", request, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Update a section within a ticket field.
        ///
        /// c.f. https://developers.freshdesk.com/api/#update_section
        /// </summary>
        /// 
        /// <param name="ticketFieldId">
        /// The unique identifier for the ticket field
        /// </param>
        ///
        /// <param name="sectionId">
        /// The unique identifier for the section to be updated
        /// </param>
        /// 
        /// <param name="request">
        /// The request encapsulating the properties to set on the section.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The updated section</returns>
        public async Task<Section> UpdateSectionAsync(
            long ticketFieldId,
            long sectionId,
            UpdateSectionRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _freshdeskClient
                .ApiOperationAsync<Section>(HttpMethod.Put, $"/api/v2/admin/ticket_fields/{ticketFieldId}/sections/{sectionId}", request, cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
