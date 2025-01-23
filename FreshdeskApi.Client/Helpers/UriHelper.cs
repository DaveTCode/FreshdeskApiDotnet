using System;
using System.Collections.Specialized;

namespace FreshdeskApi.Client.Helpers;

public static class UriHelper
{
    public static Uri CreateUri(string initialUrl, NameValueCollection queryString)
        => new(initialUrl + (queryString.Count > 0 ? $"?{queryString}" : null), UriKind.Relative);

}
