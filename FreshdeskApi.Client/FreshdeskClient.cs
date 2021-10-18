using System.Net.Http;
using System.Runtime.CompilerServices;
using FreshdeskApi.Client.Agents;
using FreshdeskApi.Client.Channel;
using FreshdeskApi.Client.Companies;
using FreshdeskApi.Client.Contacts;
using FreshdeskApi.Client.Conversations;
using FreshdeskApi.Client.Groups;
using FreshdeskApi.Client.Solutions;
using FreshdeskApi.Client.TicketFields;
using FreshdeskApi.Client.Tickets;

[assembly: InternalsVisibleTo("FreshdeskApi.Client.UnitTests")]

namespace FreshdeskApi.Client
{
    /// <summary>
    /// Wrapper for all supported API clients
    /// </summary>
    public class FreshdeskClient : IFreshdeskClient
    {
        public IFreshdeskTicketClient Tickets { get; }

        public IFreshdeskContactClient Contacts { get; }

        public IFreshdeskGroupClient Groups { get; }

        public IFreshdeskAgentClient Agents { get; }

        public IFreshdeskCompaniesClient Companies { get; }

        public IFreshdeskSolutionClient Solutions { get; }

        public IFreshdeskTicketFieldsClient TicketFields { get; }

        public IFreshdeskConversationsClient Conversations { get; }

        public IFreshdeskChannelApiClient ChannelApi { get; }

        /// <summary>
        /// Default constructor for DependencyInjection
        /// </summary>
        public FreshdeskClient(
            IFreshdeskTicketClient freshdeskTicketClient,
            IFreshdeskContactClient freshdeskContactClient,
            IFreshdeskGroupClient freshdeskGroupClient,
            IFreshdeskAgentClient freshdeskAgentClient,
            IFreshdeskCompaniesClient freshdeskCompaniesClient,
            IFreshdeskSolutionClient freshdeskSolutionClient,
            IFreshdeskTicketFieldsClient freshdeskTicketFieldsClient,
            IFreshdeskConversationsClient freshdeskConversationsClient,
            IFreshdeskChannelApiClient freshdeskChannelApiClient
        )
        {
            Tickets = freshdeskTicketClient;
            Contacts = freshdeskContactClient;
            Groups = freshdeskGroupClient;
            Agents = freshdeskAgentClient;
            Companies = freshdeskCompaniesClient;
            Solutions = freshdeskSolutionClient;
            TicketFields = freshdeskTicketFieldsClient;
            Conversations = freshdeskConversationsClient;
            ChannelApi = freshdeskChannelApiClient;
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
        public FreshdeskClient(IFreshdeskHttpClient httpClient) : this(
            new FreshdeskTicketClient(httpClient),
            new FreshdeskContactClient(httpClient),
            new FreshdeskGroupClient(httpClient),
            new FreshdeskAgentClient(httpClient),
            new FreshdeskCompaniesClient(httpClient),
            new FreshdeskSolutionClient(httpClient),
            new FreshdeskTicketFieldsClient(httpClient),
            new FreshdeskConversationsClient(httpClient),
            new FreshdeskChannelApiClient(httpClient)
        )
        {
        }

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
        // ReSharper disable once UnusedMember.Global
        public FreshdeskClient(
            string freshdeskDomain, string apiKey
        ) : this(new FreshdeskHttpClient(freshdeskDomain, apiKey))
        {
        }
    }
}
