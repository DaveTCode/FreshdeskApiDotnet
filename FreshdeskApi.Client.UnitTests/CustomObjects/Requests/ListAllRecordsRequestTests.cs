using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.CustomObjects.Requests;
using FreshdeskApi.Client.CustomObjects.Requests.Parameters;
using Xunit;

namespace FreshdeskApi.Client.UnitTests.CustomObjects.Requests;

public class ListAllRecordsRequestTests
{
    [Fact]
    public void EncodesSort()
    {
        var record = new ListAllRecordsRequest
        {
            Filters = null,
            Sort = new RecordPageRequestParameterSort
            {
                SortBy = "created_time",
                Order = ESortOrder.Asc
            }
        };

        Assert.Equal("?sort_by=created_time%3bASC", record.GetQuery());
    }

    [Fact]
    public void EncodesFilters()
    {
        var record = new ListAllRecordsRequest
        {
            Filters =
            [
                new RecordPageRequestParameterFilter(
                    "created_time",
                    EFilterOperator.Equals,
                    "2024-08-26T18:00:00.000Z"
                ),
                new RecordPageRequestParameterFilter(
                    "age",
                    EFilterOperator.GreaterThan,
                    "35"
                ),
                new RecordPageRequestParameterFilter(
                    "updated_time",
                    EFilterOperator.GreaterThan,
                    "2020-09-23T22:35:45.000Z"
                ),
            ],
            Sort = null
        };

#if NET9_0_OR_GREATER
        Assert.Equal(
            "?created_time=2024-08-26T18%3a00%3a00.000Z&age%5bgt%5d=35&updated_time%5bgt%5d=2020-09-23T22%3a35%3a45.000Z",
            record.GetQuery()
        );
#else
        Assert.Equal(
            "?created_time=2024-08-26T18%3a00%3a00.000Z&age%5Bgt%5D=35&updated_time%5Bgt%5D=2020-09-23T22%3a35%3a45.000Z",
            record.GetQuery()
        );
#endif
    }
}
