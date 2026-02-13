using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using FreshdeskApi.Client.TicketFields.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace FreshdeskApi.Client.UnitTests.TicketFields.Models;

public sealed class TicketFieldTests
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
    public void Deserialize_CustomDropdownField_WithChoices()
    {
        var ticketField = Deserialize<TicketField>(CustomDropdownFieldJson);

        Assert.Equal(22, ticketField.Id);
        Assert.Equal("cf_issue_type_297457", ticketField.Name);
        Assert.Equal("Issue Type", ticketField.Label);
        Assert.Equal("Issue Type", ticketField.LabelForCustomers);
        Assert.Equal(1, ticketField.Position);
        Assert.Equal("custom_dropdown", ticketField.Type);
        Assert.False(ticketField.Default);
        Assert.True(ticketField.CustomersCanEdit);
        Assert.False(ticketField.RequiredForClosure);
        Assert.False(ticketField.RequiredForAgents);
        Assert.False(ticketField.RequiredForCustomers);
        Assert.True(ticketField.DisplayedToCustomers);
        Assert.Equal(2019, ticketField.CreatedAt.Year);
        Assert.Equal(12, ticketField.CreatedAt.Month);
        Assert.Equal(19, ticketField.CreatedAt.Day);

        Assert.NotNull(ticketField.Choices);
        var choices = ticketField.Choices as JArray;
        Assert.NotNull(choices);
        Assert.Equal(3, choices.Count);

        var firstChoice = choices[0];
        Assert.Equal(125, firstChoice["id"]!.Value<long>());
        Assert.Equal(1, firstChoice["position"]!.Value<int>());
        Assert.Equal("Refund", firstChoice["value"]!.Value<string>());
        Assert.Equal(22, firstChoice["parent_choice_id"]!.Value<long>());
        var nestedChoices = firstChoice["choices"] as JArray;
        Assert.NotNull(nestedChoices);
        Assert.Empty(nestedChoices);

        Assert.Equal("Faulty Product", choices[1]["value"]!.Value<string>());
        Assert.Equal("Item Not Delivered", choices[2]["value"]!.Value<string>());
    }

    [Fact]
    public void Deserialize_CustomDropdownField_RoundTrip()
    {
        var ticketField = Deserialize<TicketField>(CustomDropdownFieldJson);

        var serialized = JsonConvert.SerializeObject(ticketField);
        var roundTripped = JsonConvert.DeserializeObject<TicketField>(serialized);

        Assert.NotNull(roundTripped);
        Assert.Equal(ticketField.Id, roundTripped.Id);
        Assert.Equal(ticketField.Name, roundTripped.Name);
        Assert.Equal(ticketField.Type, roundTripped.Type);

        var originalChoices = ticketField.Choices as JArray;
        var roundTrippedChoices = roundTripped.Choices as JArray;
        Assert.NotNull(originalChoices);
        Assert.NotNull(roundTrippedChoices);
        Assert.Equal(originalChoices.Count, roundTrippedChoices.Count);
    }

    [Fact]
    public void Deserialize_CustomTextField_WithSectionMappings()
    {
        var ticketField = Deserialize<TicketField>(CustomTextFieldJson);

        Assert.Equal(27, ticketField.Id);
        Assert.Equal("cf_feedback", ticketField.Name);
        Assert.Equal("Feedback", ticketField.Label);
        Assert.Equal("Feedback", ticketField.LabelForCustomers);
        Assert.Equal(17, ticketField.Position);
        Assert.Equal("custom_text", ticketField.Type);
        Assert.False(ticketField.Default);
        Assert.True(ticketField.CustomersCanEdit);
        Assert.False(ticketField.RequiredForClosure);
        Assert.False(ticketField.RequiredForAgents);
        Assert.False(ticketField.RequiredForCustomers);
        Assert.True(ticketField.DisplayedToCustomers);
        Assert.Equal(2020, ticketField.CreatedAt.Year);
        Assert.Equal(1, ticketField.CreatedAt.Month);
        Assert.Equal(27, ticketField.CreatedAt.Day);
    }

    [Fact]
    public void Deserialize_CustomTextField_RoundTrip()
    {
        var ticketField = Deserialize<TicketField>(CustomTextFieldJson);

        var serialized = JsonConvert.SerializeObject(ticketField);
        var roundTripped = JsonConvert.DeserializeObject<TicketField>(serialized);

        Assert.NotNull(roundTripped);
        Assert.Equal(ticketField.Id, roundTripped.Id);
        Assert.Equal(ticketField.Name, roundTripped.Name);
        Assert.Equal(ticketField.Type, roundTripped.Type);
    }

    [Fact]
    public void Deserialize_NestedField_WithChoicesAndDependentFields()
    {
        var wrapper = Deserialize<JObject>(NestedFieldJson);
        var ticketFieldToken = wrapper["ticket_field"];
        Assert.NotNull(ticketFieldToken);

        var ticketField = ticketFieldToken.ToObject<TicketField>();
        Assert.NotNull(ticketField);

        Assert.Equal(28, ticketField.Id);
        Assert.Equal("cf_bank964807", ticketField.Name);
        Assert.Equal("Bank", ticketField.Label);
        Assert.Equal("Bank", ticketField.LabelForCustomers);
        Assert.Equal(13, ticketField.Position);
        Assert.Equal("nested_field", ticketField.Type);
        Assert.False(ticketField.Default);
        Assert.True(ticketField.CustomersCanEdit);
        Assert.False(ticketField.RequiredForClosure);
        Assert.False(ticketField.RequiredForAgents);
        Assert.False(ticketField.RequiredForCustomers);
        Assert.True(ticketField.DisplayedToCustomers);

        Assert.NotNull(ticketField.Choices);
        var choices = ticketField.Choices as JArray;
        Assert.NotNull(choices);
        Assert.Equal(3, choices.Count);

        var hdfc = choices[0];
        Assert.Equal(6, hdfc["id"]!.Value<long>());
        Assert.Equal("HDFC", hdfc["value"]!.Value<string>());
        Assert.Equal(28, hdfc["parent_choice_id"]!.Value<long>());

        var hdfcCities = hdfc["choices"] as JArray;
        Assert.NotNull(hdfcCities);
        Assert.Equal(2, hdfcCities.Count);

        var chennai = hdfcCities[0];
        Assert.Equal("Chennai", chennai["value"]!.Value<string>());
        Assert.Equal(6, chennai["parent_choice_id"]!.Value<long>());

        var chennaiBranches = chennai["choices"] as JArray;
        Assert.NotNull(chennaiBranches);
        Assert.Equal(2, chennaiBranches.Count);
        Assert.Equal("Porur", chennaiBranches[0]["value"]!.Value<string>());
        Assert.Equal("OMR", chennaiBranches[1]["value"]!.Value<string>());

        var sbi = choices[1];
        Assert.Equal("SBI", sbi["value"]!.Value<string>());
        var sbiCities = sbi["choices"] as JArray;
        Assert.NotNull(sbiCities);
        Assert.Equal(2, sbiCities.Count);

        var icici = choices[2];
        Assert.Equal("ICICI", icici["value"]!.Value<string>());
        var iciciCities = icici["choices"] as JArray;
        Assert.NotNull(iciciCities);
        Assert.Single(iciciCities);
    }

    [Fact]
    public void Deserialize_NestedField_DependentFieldsAreAccessibleViaJObject()
    {
        // dependent_fields is not mapped on TicketField — verify raw JSON preserves them
        var wrapper = Deserialize<JObject>(NestedFieldJson);
        var ticketFieldToken = wrapper["ticket_field"]!;

        var dependentFields = ticketFieldToken["dependent_fields"] as JArray;

        Assert.NotNull(dependentFields);
        Assert.Equal(2, dependentFields.Count);

        var district = dependentFields[0];
        Assert.Equal(29, district["id"]!.Value<long>());
        Assert.Equal("cf_district", district["name"]!.Value<string>());
        Assert.Equal("District", district["label"]!.Value<string>());
        Assert.Equal(2, district["level"]!.Value<int>());
        Assert.Equal(28, district["ticket_field_id"]!.Value<long>());

        var branch = dependentFields[1];
        Assert.Equal(30, branch["id"]!.Value<long>());
        Assert.Equal("cf_branch53114", branch["name"]!.Value<string>());
        Assert.Equal("Branch", branch["label"]!.Value<string>());
        Assert.Equal(3, branch["level"]!.Value<int>());
        Assert.Equal(28, branch["ticket_field_id"]!.Value<long>());
    }

    [Fact]
    public void Deserialize_NestedField_RoundTrip()
    {
        var wrapper = Deserialize<JObject>(NestedFieldJson);
        var ticketField = wrapper["ticket_field"]!.ToObject<TicketField>();
        Assert.NotNull(ticketField);

        var serialized = JsonConvert.SerializeObject(ticketField);
        var roundTripped = JsonConvert.DeserializeObject<TicketField>(serialized);

        Assert.NotNull(roundTripped);
        Assert.Equal(ticketField.Id, roundTripped.Id);
        Assert.Equal(ticketField.Name, roundTripped.Name);
        Assert.Equal(ticketField.Type, roundTripped.Type);

        var originalChoices = ticketField.Choices as JArray;
        var roundTrippedChoices = roundTripped.Choices as JArray;
        Assert.NotNull(originalChoices);
        Assert.NotNull(roundTrippedChoices);
        Assert.Equal(originalChoices.Count, roundTrippedChoices.Count);

        var originalHdfc = originalChoices[0]["choices"] as JArray;
        var roundTrippedHdfc = roundTrippedChoices[0]["choices"] as JArray;
        Assert.NotNull(originalHdfc);
        Assert.NotNull(roundTrippedHdfc);
        Assert.Equal(originalHdfc.Count, roundTrippedHdfc.Count);
    }

    [Fact]
    public void Deserialize_NestedField_LeafChoicesHaveEmptyChildArray()
    {
        var wrapper = Deserialize<JObject>(NestedFieldJson);
        var ticketField = wrapper["ticket_field"]!.ToObject<TicketField>();
        Assert.NotNull(ticketField);

        var choices = ticketField.Choices as JArray;
        Assert.NotNull(choices);

        var porur = (choices[0]["choices"] as JArray)![0]["choices"]![0]!;
        Assert.Equal("Porur", porur["value"]!.Value<string>());

        var porurChildren = porur["choices"] as JArray;
        Assert.NotNull(porurChildren);
        Assert.Empty(porurChildren);
    }

    #region Test Data

    [StringSyntax(StringSyntaxAttribute.Json)]
    private const string CustomDropdownFieldJson =
        """
        {
            "id": 22,
            "name": "cf_issue_type_297457",
            "label": "Issue Type",
            "label_for_customers": "Issue Type",
            "position": 1,
            "type": "custom_dropdown",
            "default": false,
            "customers_can_edit": true,
            "required_for_closure": false,
            "required_for_agents": false,
            "required_for_customers": false,
            "displayed_to_customers": true,
            "created_at": "2019-12-19T05:28:31Z",
            "updated_at": "2019-12-19T05:28:31Z",
            "choices": [
                {
                    "id": 125,
                    "position": 1,
                    "value": "Refund",
                    "parent_choice_id": 22,
                    "choices": []
                },
                {
                    "id": 126,
                    "position": 2,
                    "value": "Faulty Product",
                    "parent_choice_id": 22,
                    "choices": []
                },
                {
                    "id": 127,
                    "position": 3,
                    "value": "Item Not Delivered",
                    "parent_choice_id": 22,
                    "choices": []
                }
            ]
        }
        """;

    [StringSyntax(StringSyntaxAttribute.Json)]
    private const string CustomTextFieldJson =
        """
        {
            "id": 27,
            "name": "cf_feedback",
            "label": "Feedback",
            "label_for_customers": "Feedback",
            "position": 17,
            "type": "custom_text",
            "default": false,
            "customers_can_edit": true,
            "required_for_closure": false,
            "required_for_agents": false,
            "required_for_customers": false,
            "displayed_to_customers": true,
            "created_at": "2020-01-27T07:12:48Z",
            "updated_at": "2020-01-27T07:12:48Z",
            "archived": false,
            "section_mappings": [
                {
                    "section_id": 1,
                    "position": 3
                }
            ]
        }
        """;

    [StringSyntax(StringSyntaxAttribute.Json)]
    private const string NestedFieldJson =
        """
        {
            "ticket_field": {
                "id": 28,
                "name": "cf_bank964807",
                "label": "Bank",
                "label_for_customers": "Bank",
                "position": 13,
                "type": "nested_field",
                "default": false,
                "customers_can_edit": true,
                "required_for_closure": false,
                "required_for_agents": false,
                "required_for_customers": false,
                "displayed_to_customers": true,
                "created_at": "2020-01-27T07:19:59Z",
                "updated_at": "2020-01-27T07:19:59Z",
                "archived": false,
                "choices": [
                    {
                        "id": 6,
                        "position": 1,
                        "value": "HDFC",
                        "parent_choice_id": 28,
                        "choices": [
                            {
                                "id": 7,
                                "position": 1,
                                "value": "Chennai",
                                "parent_choice_id": 6,
                                "choices": [
                                    {
                                        "id": 8,
                                        "position": 1,
                                        "value": "Porur",
                                        "parent_choice_id": 7,
                                        "choices": []
                                    },
                                    {
                                        "id": 9,
                                        "position": 2,
                                        "value": "OMR",
                                        "parent_choice_id": 7,
                                        "choices": []
                                    }
                                ]
                            },
                            {
                                "id": 10,
                                "position": 2,
                                "value": "Madurai",
                                "parent_choice_id": 6,
                                "choices": [
                                    {
                                        "id": 11,
                                        "position": 1,
                                        "value": "Thirumangalam",
                                        "parent_choice_id": 10,
                                        "choices": []
                                    },
                                    {
                                        "id": 12,
                                        "position": 2,
                                        "value": "SB Colony",
                                        "parent_choice_id": 10,
                                        "choices": []
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "id": 13,
                        "position": 2,
                        "value": "SBI",
                        "parent_choice_id": 28,
                        "choices": [
                            {
                                "id": 14,
                                "position": 1,
                                "value": "Chennai",
                                "parent_choice_id": 13,
                                "choices": [
                                    {
                                        "id": 15,
                                        "position": 1,
                                        "value": "Tambaram",
                                        "parent_choice_id": 14,
                                        "choices": []
                                    },
                                    {
                                        "id": 16,
                                        "position": 2,
                                        "value": "Guindy",
                                        "parent_choice_id": 14,
                                        "choices": []
                                    }
                                ]
                            },
                            {
                                "id": 17,
                                "position": 2,
                                "value": "Trichy",
                                "parent_choice_id": 13,
                                "choices": [
                                    {
                                        "id": 18,
                                        "position": 1,
                                        "value": "AGS Colony",
                                        "parent_choice_id": 17,
                                        "choices": []
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "id": 19,
                        "position": 3,
                        "value": "ICICI",
                        "parent_choice_id": 28,
                        "choices": [
                            {
                                "id": 20,
                                "position": 1,
                                "value": "Chennai",
                                "parent_choice_id": 19,
                                "choices": [
                                    {
                                        "id": 21,
                                        "position": 1,
                                        "value": "Avadi",
                                        "parent_choice_id": 20,
                                        "choices": []
                                    }
                                ]
                            }
                        ]
                    }
                ],
                "dependent_fields": [
                    {
                        "id": 29,
                        "name": "cf_district",
                        "label": "District",
                        "label_for_customers": "District",
                        "level": 2,
                        "ticket_field_id": 28,
                        "created_at": "2020-01-27T07:19:59Z",
                        "updated_at": "2020-01-27T07:19:59Z"
                    },
                    {
                        "id": 30,
                        "name": "cf_branch53114",
                        "label": "Branch",
                        "label_for_customers": "Branch",
                        "level": 3,
                        "ticket_field_id": 28,
                        "created_at": "2020-01-27T07:19:59Z",
                        "updated_at": "2020-01-27T07:19:59Z"
                    }
                ]
            }
        }
        """;

    #endregion
}
