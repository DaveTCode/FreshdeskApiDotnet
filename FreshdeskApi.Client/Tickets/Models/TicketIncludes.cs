using System.Text;

namespace FreshdeskApi.Client.Tickets.Models
{
    /// <summary>
    /// Optionally include extra data about a ticket on API calls which return one.
    ///
    /// Each of these costs 1 extra API credit on a call which returns a
    /// single ticket and 2 extra API credits on a call which returns a list
    /// of tickets.
    /// </summary>
    public struct TicketIncludes
    {
        /// <summary>
        /// Causes the company information (id, name) to be retrieved for a
        /// given ticket.
        /// </summary>
        public bool Company;

        /// <summary>
        /// Causes the full conversation history to be loaded for a given
        /// ticket.
        /// </summary>
        public bool Conversations;

        /// <summary>
        /// Causes the full requester information to be loaded for a given
        /// ticket.
        /// </summary>
        public bool Requester;

        /// <summary>
        /// Causes statistics to be loaded for a given ticket.
        /// </summary>
        public bool Stats;

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Company) sb.Append(",company");
            if (Conversations) sb.Append(",conversations");
            if (Requester) sb.Append(",requester");
            if (Stats) sb.Append(",stats");

            return sb.ToString().TrimStart(',');
        }
    }
}
