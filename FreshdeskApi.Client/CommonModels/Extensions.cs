using System;

namespace FreshdeskApi.Client.CommonModels;

public static class Extensions
{
    public static string ToQuery(this ESortOrder sortOrder) => sortOrder switch
    {
        ESortOrder.Asc => "ASC",
        ESortOrder.Desc => "DESC",
        _ => throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null),
    };

    public static string ToQueryParameter(this EFilterOperator sortOrder) =>
#if NETSTANDARD2_1 || NET8_0
        Uri.EscapeDataString
#endif
        (sortOrder switch
        {
            EFilterOperator.Equals => "",
            EFilterOperator.GreaterThan => "[gt]",
            EFilterOperator.LessThan => "[lt]",
            EFilterOperator.GreaterThanOrEqual => "[gte]",
            EFilterOperator.LessThanOrEqual => "[lte]",
            _ => throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null),
        });
}
