using FreshdeskApi.Client.Agents;
using FreshdeskApi.Client.Channel;
using FreshdeskApi.Client.Companies;
using FreshdeskApi.Client.Contacts;
using FreshdeskApi.Client.Conversations;
using FreshdeskApi.Client.Groups;
using FreshdeskApi.Client.Products;
using FreshdeskApi.Client.Solutions;
using FreshdeskApi.Client.TicketFields;
using FreshdeskApi.Client.Tickets;

namespace FreshdeskApi.Client
{
    public interface IFreshdeskClient
    {
        IFreshdeskTicketClient Tickets { get; }

        IFreshdeskContactClient Contacts { get; }

        IFreshdeskGroupClient Groups { get; }
        
        IFreshdeskProductClient Products { get; }

        IFreshdeskAgentClient Agents { get; }

        IFreshdeskCompaniesClient Companies { get; }

        IFreshdeskSolutionClient Solutions { get; }

        IFreshdeskTicketFieldsClient TicketFields { get; }

        IFreshdeskConversationsClient Conversations { get; }

        IFreshdeskChannelApiClient ChannelApi { get; }
    }
}
