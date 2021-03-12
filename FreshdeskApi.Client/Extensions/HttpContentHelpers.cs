namespace FreshdeskApi.Client.Extensions
{
    public static class HttpContentHelpers
    {
        /// <summary>
        /// Determines if the request should be serialized as JSON or a MultiPart Form Data
        /// </summary>
        /// <param name="body">Body object to be serialized</param>
        /// <returns>Boolean indicating if the body should be serialized as JSON.</returns>
        internal static bool IsMultipartFormDataRequired<TRequest>(this TRequest body)
        {
            if (body is IRequestWithAttachment requestWithAttachment)
            {
                return requestWithAttachment.IsMultipartFormDataRequired();
            }

            return true;
        }
    }
}
