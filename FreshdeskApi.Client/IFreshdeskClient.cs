using FreshdeskApi.Client.Agents;
using FreshdeskApi.Client.Channel;
using FreshdeskApi.Client.Companies;
using FreshdeskApi.Client.Contacts;
using FreshdeskApi.Client.Conversations;
using FreshdeskApi.Client.Groups;
using FreshdeskApi.Client.Solutions;
using FreshdeskApi.Client.TicketFields;
using FreshdeskApi.Client.Tickets;
using System;

namespace FreshdeskApi.Client
{
    public interface IFreshdeskClient : IDisposable
    {
        long RateLimitRemaining { get; }

        long RateLimitTotal { get; }

        FreshdeskTicketClient Tickets { get; }

        FreshdeskContactClient Contacts { get; }

        FreshdeskGroupClient Groups { get; }

        FreshdeskAgentClient Agents { get; }

        FreshdeskCompaniesClient Companies { get; }

        FreshdeskSolutionClient Solutions { get; }

        TicketFieldsClient TicketFields { get; }

        ConversationsClient Conversations { get; }

        ChannelApiClient ChannelApi { get; }
    }
}
