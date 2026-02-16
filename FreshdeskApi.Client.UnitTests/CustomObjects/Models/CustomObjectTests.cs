using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using FreshdeskApi.Client.CustomObjects.Models;
using Newtonsoft.Json;
using Xunit;

namespace FreshdeskApi.Client.UnitTests.CustomObjects.Models;

public sealed class CustomObjectTests
{
    private static T Deserialize<T>(string json)
    {
        var stream = new MemoryStream();

        using (var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true))
        {
            writer.Write(json);
        }

        stream.Position = 0;

        using var reader = new StreamReader(stream);
        using var jsonReader = new JsonTextReader(reader);
        var serializer = new JsonSerializer();

        var result = serializer.Deserialize<T>(jsonReader);
        Assert.NotNull(result);
        return result;
    }

    [Fact]
    public void Deserialize_CustomObject_WithFields()
    {
        var customObject = Deserialize<CustomObject>(CustomObjectJson);

        Assert.Equal("30", customObject.Id);
        Assert.Equal("Bookings", customObject.Name);
        Assert.Equal("BKG", customObject.Prefix);
        Assert.Null(customObject.Description);
        Assert.Equal(1, customObject.Version);
        Assert.False(customObject.Deleted);
        Assert.Equal(2021, customObject.CreatedTime.Year);
        Assert.Equal(2021, customObject.UpdatedTime.Year);

        Assert.NotNull(customObject.Fields);
        Assert.Equal(4, customObject.Fields.Length);
    }

    [Fact]
    public void Deserialize_CustomObject_PrimaryField()
    {
        var customObject = Deserialize<CustomObject>(CustomObjectJson);
        var field = customObject.Fields!.First(f => f.Name == "customer_id");

        Assert.Equal("09ec0f82-6908-460f-8428-786afa208789", field.Id);
        Assert.Equal("Customer ID", field.Label);
        Assert.Equal("PRIMARY", field.Type);
        Assert.Equal(1, field.Position);
        Assert.True(field.Required);
        Assert.True(field.Editable);
        Assert.False(field.Deleted);
        Assert.Null(field.Placeholder);
        Assert.Null(field.Hint);
        Assert.True(field.Filterable);
        Assert.True(field.Searchable);
        Assert.Null(field.ParentId);
        Assert.NotNull(field.Choices);
        Assert.Empty(field.Choices);
    }

    [Fact]
    public void Deserialize_CustomObject_TextField()
    {
        var customObject = Deserialize<CustomObject>(CustomObjectJson);
        var field = customObject.Fields!.First(f => f.Name == "customer_name");

        Assert.Equal("e573039c-d7c2-481d-8cbb-5ea3a261881d", field.Id);
        Assert.Equal("Customer Name", field.Label);
        Assert.Equal("TEXT", field.Type);
        Assert.Equal(2, field.Position);
        Assert.True(field.Required);
        Assert.True(field.Editable);
        Assert.False(field.Deleted);
        Assert.True(field.Filterable);
        Assert.True(field.Searchable);
    }

    [Fact]
    public void Deserialize_CustomObject_NumberField()
    {
        var customObject = Deserialize<CustomObject>(CustomObjectJson);
        var field = customObject.Fields!.First(f => f.Name == "age");

        Assert.Equal("f967039c-d7c2-481d-8cbb-5ea3a261783d", field.Id);
        Assert.Equal("Age", field.Label);
        Assert.Equal("NUMBER", field.Type);
        Assert.Equal(3, field.Position);
    }

    [Fact]
    public void Deserialize_CustomObject_DropdownFieldWithChoices()
    {
        var customObject = Deserialize<CustomObject>(CustomObjectJson);
        var field = customObject.Fields!.First(f => f.Name == "booking_status");

        Assert.Equal("a1b2c3d4-e5f6-7890-abcd-ef1234567890", field.Id);
        Assert.Equal("Booking Status", field.Label);
        Assert.Equal("DROPDOWN", field.Type);
        Assert.Equal(4, field.Position);
        Assert.False(field.Required);
        Assert.True(field.Editable);
        Assert.False(field.Deleted);
        Assert.Equal("Select a status", field.Placeholder);
        Assert.Equal("Current status of the booking", field.Hint);
        Assert.True(field.Filterable);
        Assert.False(field.Searchable);
        Assert.Null(field.ParentId);

        Assert.NotNull(field.Choices);
        Assert.Equal(3, field.Choices.Count);

        var choices = field.Choices.ToList();
        Assert.Equal(1, choices[0].Id);
        Assert.Equal("Active", choices[0].Value);
        Assert.Equal(1, choices[0].Position);

        Assert.Equal(2, choices[1].Id);
        Assert.Equal("Obsolete", choices[1].Value);
        Assert.Equal(2, choices[1].Position);

        Assert.Equal(3, choices[2].Id);
        Assert.Equal("Draft", choices[2].Value);
        Assert.Equal(3, choices[2].Position);
    }

    [Fact]
    public void Deserialize_CustomObject_RoundTrip()
    {
        var customObject = Deserialize<CustomObject>(CustomObjectJson);

        var serialized = JsonConvert.SerializeObject(customObject);
        var roundTripped = JsonConvert.DeserializeObject<CustomObject>(serialized);

        Assert.NotNull(roundTripped);
        Assert.Equal(customObject.Id, roundTripped.Id);
        Assert.Equal(customObject.Name, roundTripped.Name);
        Assert.Equal(customObject.Prefix, roundTripped.Prefix);
        Assert.Equal(customObject.Version, roundTripped.Version);
        Assert.Equal(customObject.Deleted, roundTripped.Deleted);
        Assert.Equal(customObject.CreatedTime, roundTripped.CreatedTime);
        Assert.Equal(customObject.UpdatedTime, roundTripped.UpdatedTime);

        Assert.NotNull(roundTripped.Fields);
        Assert.Equal(customObject.Fields!.Length, roundTripped.Fields.Length);

        var originalDropdown = customObject.Fields.First(f => f.Name == "booking_status");
        var roundTrippedDropdown = roundTripped.Fields.First(f => f.Name == "booking_status");
        Assert.Equal(originalDropdown.Choices!.Count, roundTrippedDropdown.Choices!.Count);
        Assert.Equal(
            originalDropdown.Choices.First().Value,
            roundTrippedDropdown.Choices.First().Value);
    }

    [Fact]
    public void Deserialize_ListCustomObjectsResponse()
    {
        var response = Deserialize<ListCustomObjectsResponse>(ListCustomObjectsResponseJson);

        Assert.NotNull(response.Schemas);
        Assert.Single(response.Schemas);

        var schema = response.Schemas.First();
        Assert.Equal("30", schema.Id);
        Assert.Equal("Bookings", schema.Name);
        Assert.NotNull(schema.Fields);
        Assert.Equal(4, schema.Fields.Length);
    }

    #region Test Data

    [StringSyntax(StringSyntaxAttribute.Json)]
    private const string CustomObjectJson =
        """
        {
            "name": "Bookings",
            "prefix": "BKG",
            "description": null,
            "version": 1,
            "deleted": false,
            "id": 30,
            "created_time": 1624081385288,
            "updated_time": 1624081385288,
            "fields": [
                {
                    "id": "09ec0f82-6908-460f-8428-786afa208789",
                    "name": "customer_id",
                    "label": "Customer ID",
                    "type": "PRIMARY",
                    "position": 1,
                    "required": true,
                    "editable": true,
                    "visible": true,
                    "deleted": false,
                    "placeholder": null,
                    "hint": null,
                    "filterable": true,
                    "searchable": true,
                    "parent_id": null,
                    "choices": []
                },
                {
                    "id": "e573039c-d7c2-481d-8cbb-5ea3a261881d",
                    "name": "customer_name",
                    "label": "Customer Name",
                    "type": "TEXT",
                    "position": 2,
                    "required": true,
                    "editable": true,
                    "visible": true,
                    "deleted": false,
                    "placeholder": null,
                    "hint": null,
                    "filterable": true,
                    "searchable": true,
                    "parent_id": null,
                    "choices": []
                },
                {
                    "id": "f967039c-d7c2-481d-8cbb-5ea3a261783d",
                    "name": "age",
                    "label": "Age",
                    "type": "NUMBER",
                    "position": 3,
                    "required": true,
                    "editable": true,
                    "visible": true,
                    "deleted": false,
                    "placeholder": null,
                    "hint": null,
                    "filterable": true,
                    "searchable": true,
                    "parent_id": null,
                    "choices": []
                },
                {
                  "id" : "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
                  "name" : "booking_status",
                  "label" : "Booking Status",
                  "type" : "DROPDOWN",
                  "position" : 4,
                  "required" : false,
                  "editable" : true,
                  "visible" : true,
                  "deleted" : false,
                  "placeholder" : "Select a status",
                  "hint": "Current status of the booking",
                  "field_options" : {
                    "unique" : "false"
                  },
                  "filterable" : true,
                  "searchable" : false,
                  "aggregatable" : false,
                  "has_dependents" : false,
                  "parent_id" : null,
                  "choices" : [ {
                    "id" : 1,
                    "value" : "Active",
                    "position" : 1,
                    "dependent_ids" : { }
                  }, {
                    "id" : 2,
                    "value" : "Obsolete",
                    "position" : 2,
                    "dependent_ids" : { }
                  }, {
                    "id" : 3,
                    "value" : "Draft",
                    "position" : 3,
                    "dependent_ids" : { }
                  } ],
                  "default" : null,
                  "validations" : { },
                  "created_time" : 1768896573000,
                  "updated_time" : 1769470228000
                }
            ]
        }
        """;

    [StringSyntax(StringSyntaxAttribute.Json)]
    private const string ListCustomObjectsResponseJson =
        """
        {
            "schemas": [
                {
                    "name": "Bookings",
                    "prefix": "BKG",
                    "description": null,
                    "version": 1,
                    "deleted": false,
                    "id": 30,
                    "created_time": 1624081385288,
                    "updated_time": 1624081385288,
                    "fields": [
                        {
                            "id": "09ec0f82-6908-460f-8428-786afa208789",
                            "name": "customer_id",
                            "label": "Customer ID",
                            "type": "PRIMARY",
                            "position": 1,
                            "required": true,
                            "editable": true,
                            "visible": true,
                            "deleted": false,
                            "placeholder": null,
                            "hint": null,
                            "filterable": true,
                            "searchable": true,
                            "parent_id": null,
                            "choices": []
                        },
                        {
                            "id": "e573039c-d7c2-481d-8cbb-5ea3a261881d",
                            "name": "customer_name",
                            "label": "Customer Name",
                            "type": "TEXT",
                            "position": 2,
                            "required": true,
                            "editable": true,
                            "visible": true,
                            "deleted": false,
                            "placeholder": null,
                            "hint": null,
                            "filterable": true,
                            "searchable": true,
                            "parent_id": null,
                            "choices": []
                        },
                        {
                            "id": "f967039c-d7c2-481d-8cbb-5ea3a261783d",
                            "name": "age",
                            "label": "Age",
                            "type": "NUMBER",
                            "position": 3,
                            "required": true,
                            "editable": true,
                            "visible": true,
                            "deleted": false,
                            "placeholder": null,
                            "hint": null,
                            "filterable": true,
                            "searchable": true,
                            "parent_id": null,
                            "choices": []
                        },
                        {
                            "id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
                            "name": "booking_status",
                            "label": "Booking Status",
                            "type": "DROPDOWN",
                            "position": 4,
                            "required": false,
                            "editable": true,
                            "visible": true,
                            "deleted": false,
                            "placeholder": "Select a status",
                            "hint": "Current status of the booking",
                            "filterable": true,
                            "searchable": false,
                            "parent_id": null,
                            "choices": [
                                {
                                    "id": 1,
                                    "value": "Pending",
                                    "position": 1
                                },
                                {
                                    "id": 2,
                                    "value": "Confirmed",
                                    "position": 2
                                },
                                {
                                    "id": 3,
                                    "value": "Cancelled",
                                    "position": 3
                                }
                            ]
                        }
                    ]
                }
            ]
        }
        """;

    #endregion
}
