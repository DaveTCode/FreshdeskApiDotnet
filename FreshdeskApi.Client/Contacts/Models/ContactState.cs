using System;
using System.Diagnostics.CodeAnalysis;

namespace FreshdeskApi.Client.Contacts.Models
{
    /// <summary>
    /// All valid options for a contact state, as used when filtering the list
    /// of contacts.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum ContactState
    {
        Blocked,
        Deleted,
        Unverified,
        Verified
    }

    public static class ContractStateExtensions
    {
        public static string GetQueryStringValue(this ContactState state) => state switch
        {
            ContactState.Blocked => "blocked",
            ContactState.Deleted => "deleted",
            ContactState.Unverified => "unverified",
            ContactState.Verified => "verified",
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}
