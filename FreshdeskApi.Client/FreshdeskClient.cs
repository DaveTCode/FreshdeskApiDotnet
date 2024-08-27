using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using FreshdeskApi.Client.Agents;
using FreshdeskApi.Client.Attachments;
using FreshdeskApi.Client.Channel;
using FreshdeskApi.Client.Companies;
using FreshdeskApi.Client.Contacts;
using FreshdeskApi.Client.Conversations;
using FreshdeskApi.Client.CustomObjects;
using FreshdeskApi.Client.Groups;
using FreshdeskApi.Client.Me;
using FreshdeskApi.Client.Products;
using FreshdeskApi.Client.Roles;
using FreshdeskApi.Client.Solutions;
using FreshdeskApi.Client.TicketFields;
using FreshdeskApi.Client.Tickets;

namespace FreshdeskApi.Client;

/// <summary>
/// Wrapper for all supported API clients
/// </summary>
public class FreshdeskClient : IFreshdeskClient
{
    public IFreshdeskTicketClient Tickets { get; }

    public IFreshdeskContactClient Contacts { get; }

    public IFreshdeskGroupClient Groups { get; }

    public IFreshdeskRoleClient Roles { get; }

    public IFreshdeskProductClient Products { get; }

    public IFreshdeskMeClient Me { get; }

    public IFreshdeskAgentClient Agents { get; }

    public IFreshdeskCompaniesClient Companies { get; }

    public IFreshdeskSolutionClient Solutions { get; }

    public IFreshdeskTicketFieldsClient TicketFields { get; }

    public IFreshdeskConversationsClient Conversations { get; }

    public IFreshdeskChannelApiClient ChannelApi { get; }

    public IFreshdeskAttachmentsClient Attachments { get; }

    public IFreshdeskCustomObjectClient CustomObjects { get; }

    /// <summary>
    /// Default constructor for DependencyInjection
    /// </summary>
    public FreshdeskClient(
        IFreshdeskTicketClient freshdeskTicketClient,
        IFreshdeskContactClient freshdeskContactClient,
        IFreshdeskGroupClient freshdeskGroupClient,
        IFreshdeskRoleClient freshdeskRoleClient,
        IFreshdeskProductClient freshdeskProductClient,
        IFreshdeskMeClient freshdeskMeClient,
        IFreshdeskAgentClient freshdeskAgentClient,
        IFreshdeskCompaniesClient freshdeskCompaniesClient,
        IFreshdeskSolutionClient freshdeskSolutionClient,
        IFreshdeskTicketFieldsClient freshdeskTicketFieldsClient,
        IFreshdeskConversationsClient freshdeskConversationsClient,
        IFreshdeskChannelApiClient freshdeskChannelApiClient,
        IFreshdeskAttachmentsClient freshdeskAttachmentsClient,
        IFreshdeskCustomObjectClient freshdeskCustomObjectClient
    )
    {
        Tickets = freshdeskTicketClient;
        Contacts = freshdeskContactClient;
        Groups = freshdeskGroupClient;
        Roles = freshdeskRoleClient;
        Products = freshdeskProductClient;
        Me = freshdeskMeClient;
        Agents = freshdeskAgentClient;
        Companies = freshdeskCompaniesClient;
        Solutions = freshdeskSolutionClient;
        TicketFields = freshdeskTicketFieldsClient;
        Conversations = freshdeskConversationsClient;
        ChannelApi = freshdeskChannelApiClient;
        Attachments = freshdeskAttachmentsClient;
        CustomObjects = freshdeskCustomObjectClient;
    }

    /// <summary>
    /// Construct a freshdesk client object when you already have access
    /// to HttpClient objects or want to otherwise pool them outside of
    /// this client.
    ///
    /// It is recommended that you use this method in long lived
    /// applications where many of these clients will be created.
    /// </summary>
    /// <param name="httpClient">
    /// A HttpClient object with authentication and
    /// <seealso cref="HttpClient.BaseAddress"/> already set.
    /// </param>
    public static FreshdeskClient Create(IFreshdeskHttpClient httpClient) => new(
        new FreshdeskTicketClient(httpClient),
        new FreshdeskContactClient(httpClient),
        new FreshdeskGroupClient(httpClient),
        new FreshdeskRoleClient(httpClient),
        new FreshdeskProductClient(httpClient),
        new FreshdeskMeClient(httpClient),
        new FreshdeskAgentClient(httpClient),
        new FreshdeskCompaniesClient(httpClient),
        new FreshdeskSolutionClient(httpClient),
        new FreshdeskTicketFieldsClient(httpClient),
        new FreshdeskConversationsClient(httpClient),
        new FreshdeskChannelApiClient(httpClient),
        new FreshdeskAttachmentsClient(httpClient),
        new FreshdeskCustomObjectClient(httpClient)
    );

    /// <summary>
    /// Construct a freshdesk client object from just domain and api key.
    ///
    /// This object wil encapsulate a single <see cref="HttpClient"/>
    /// object and is therefore disposable. All of the normal issues with
    /// HttpClient are carried over.
    /// </summary>
    /// 
    /// <param name="freshdeskDomain">
    /// The full domain on which your Freshdesk account is hosted. e.g.
    /// https://yourdomain.freshdesk.com. This must include the
    /// scheme (http/https)
    /// </param>
    ///
    /// <param name="apiKey">
    /// The API key from freshdesk of a user with sufficient permissions to
    /// perform whichever operations you are calling.
    /// </param>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static FreshdeskClient Create(
        string freshdeskDomain, string apiKey
    ) => Create(
#pragma warning disable CA2000 // Helper method for testing purposes, ignoring dispose issue
        FreshdeskHttpClient.Create(freshdeskDomain, apiKey)
#pragma warning restore CA2000
    );
}
