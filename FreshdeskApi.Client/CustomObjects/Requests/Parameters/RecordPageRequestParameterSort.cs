using FreshdeskApi.Client.CommonModels;

namespace FreshdeskApi.Client.CustomObjects.Requests.Parameters;

public record RecordPageRequestParameterSort
{
    /// <summary>
    /// The sorting property
    /// Case sensitive. It seems like you need to use an all lower-case string.
    /// You can also apply sorting on created_time and updated_time
    /// </summary>
    public string? SortBy { get; init; }

    public ESortOrder Order { get; init; }

    public string QueryStringParameterName => "sort_by";

    public string QueryStringParameterValue => $"{SortBy};{Order.ToQuery()}";
}
