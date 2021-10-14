using System;

namespace FreshdeskApi.Client.Agents.Models
{
    /// <summary>
    /// Agents in Freshdesk can be “full-time” or “occasional”. Full time
    /// agents are those in your core support team who will log in to your
    /// help desk every day. Occasional agents are those who would only need
    /// to log in a few times every month, such as the CEO or field staff.
    /// </summary>
    public enum AgentState
    {
        Fulltime,
        Occasional
    }

    public static class AgentStateExtensions
    {
        public static string GetQueryStringValue(this AgentState state) => state switch
        {
            AgentState.Fulltime => "fulltime",
            AgentState.Occasional => "occasional",
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}
