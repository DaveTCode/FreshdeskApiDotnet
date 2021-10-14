using System;
using FreshdeskApi.Client.Agents;
using FreshdeskApi.Client.Channel;
using FreshdeskApi.Client.Companies;
using FreshdeskApi.Client.Contacts;
using FreshdeskApi.Client.Conversations;
using FreshdeskApi.Client.Groups;
using FreshdeskApi.Client.Solutions;
using FreshdeskApi.Client.TicketFields;
using FreshdeskApi.Client.Tickets;

namespace FreshdeskApi.Client
{
    public interface IFreshdeskClient : IDisposable
    {
        long RateLimitRemaining { get; }

        long RateLimitTotal { get; }

        IFreshdeskTicketClient Tickets { get; }

        IFreshdeskContactClient Contacts { get; }

        IFreshdeskGroupClient Groups { get; }

        IFreshdeskAgentClient Agents { get; }

        IFreshdeskCompaniesClient Companies { get; }

        IFreshdeskSolutionClient Solutions { get; }

        ITicketFieldsClient TicketFields { get; }

        IConversationsClient Conversations { get; }

        IChannelApiClient ChannelApi { get; }
    }
}
