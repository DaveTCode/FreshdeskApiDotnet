namespace FreshdeskApi.Client.Conversations.Models
{
    /// <summary>
    /// Different sources from which a conversation entry can come from.
    ///
    /// c.f. https://developers.freshdesk.com/api/#conversations
    /// </summary>
    public enum ConversationSource
    {
        Reply = 0,
        Note = 2,
        Tweets = 5,
        SurveyFeedback = 6,
        FacebookPost = 7,
        ForwardedEmail = 8,
        Phone = 9,
        Mobihelp = 10,
        ECommerce = 11
    }
}
