using System;

namespace FreshdeskApi.Client.Agents.Models
{
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
