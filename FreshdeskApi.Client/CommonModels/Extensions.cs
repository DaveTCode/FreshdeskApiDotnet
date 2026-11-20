using System;
#if NETSTANDARD2_1
using System.Web;
#endif

namespace FreshdeskApi.Client.CommonModels;

public static class Extensions
{
#if NETSTANDARD2_1
    // Since .NET 9, HttpUtility.ParseQueryString(...).ToString() encodes the keys too.
    // The netstandard2.1 build also runs on .NET 9 and later, so it checks the runtime once.
    private static readonly bool QueryStringEncodesKeys = CheckQueryStringEncodesKeys();

    private static bool CheckQueryStringEncodesKeys()
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString.Add("[", string.Empty);

        return queryString.ToString() != "[=";
    }
#endif

    public static string ToQuery(this ESortOrder sortOrder) => sortOrder switch
    {
        ESortOrder.Asc => "ASC",
        ESortOrder.Desc => "DESC",
        _ => throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null),
    };

    public static string ToQueryParameter(this EFilterOperator sortOrder)
    {
        var queryParameter = sortOrder switch
        {
            EFilterOperator.Equals => "",
            EFilterOperator.GreaterThan => "[gt]",
            EFilterOperator.LessThan => "[lt]",
            EFilterOperator.GreaterThanOrEqual => "[gte]",
            EFilterOperator.LessThanOrEqual => "[lte]",
            _ => throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null),
        };

#if NETSTANDARD2_1
        return QueryStringEncodesKeys ? queryParameter : Uri.EscapeDataString(queryParameter);
#else
        return queryParameter;
#endif
    }
}
