using System.Net.Http;
using System.Runtime.CompilerServices;
using FreshdeskApi.Client.Agents;
using FreshdeskApi.Client.Channel;
using FreshdeskApi.Client.CommonModels;
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
    // ReSharper disable once RedundantExtendsListEntry
    public class FreshdeskClient : IFreshdeskClient
    {
        public IFreshdeskTicketClient Tickets { get; }

        public IFreshdeskContactClient Contacts { get; }

        public IFreshdeskGroupClient Groups { get; }

        public IFreshdeskAgentClient Agents { get; }

        public IFreshdeskCompaniesClient Companies { get; }

        public IFreshdeskSolutionClient Solutions { get; }

        public ITicketFieldsClient TicketFields { get; }

        public IConversationsClient Conversations { get; }

        public IChannelApiClient ChannelApi { get; }

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
        public FreshdeskClient(IFreshdeskHttpClient httpClient)
        {
            Tickets = new FreshdeskTicketClient(httpClient);
            Contacts = new FreshdeskContactClient(httpClient);
            Groups = new FreshdeskGroupClient(httpClient);
            Agents = new FreshdeskAgentClient(httpClient);
            Companies = new FreshdeskCompaniesClient(httpClient);
            Solutions = new FreshdeskSolutionClient(httpClient);
            TicketFields = new TicketFieldsClient(httpClient);
            Conversations = new ConversationsClient(httpClient);
            ChannelApi = new ChannelApiClient(httpClient);
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
