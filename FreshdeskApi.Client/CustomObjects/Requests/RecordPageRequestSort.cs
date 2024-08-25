namespace FreshdeskApi.Client.CustomObjects.Requests;

public class RecordPageRequestSort
{
    /// <summary>
    /// The sorting property
    /// Case sensitive. It seems like you need to use an all lower-case string.
    /// You can also apply sorting on created_time and updated_time
    /// </summary>
    public string? SortBy { get; set; }
    public RecordPageRequestSortOrder Order { get; set; }
    
    public string QueryStringParameterName => $"sort_by";
    public string QueryStringParameterValue => $"{SortBy};{Order}";

}
public enum RecordPageRequestSortOrder
{
    Asc,
    Desc
}
