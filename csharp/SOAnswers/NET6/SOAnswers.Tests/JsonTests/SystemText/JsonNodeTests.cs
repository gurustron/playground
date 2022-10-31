using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SOAnswers.Tests.JsonTests.SystemText;

public class JsonNodeTests
{
    [Test]
    public void Test()
    {

        string jsonString = @"{
      ""Date"": ""2019-08-01T00:00:00"",
      ""Temperature"": 25,
      ""Summary"": ""Hot"",
      ""DatesAvailable"": [
        ""2019-08-01T00:00:00"",
        ""2019-08-02T00:00:00""
      ],
      ""TemperatureRanges"": {
          ""Cold"": {
              ""High"": 20,
              ""Low"": -10
          },
          ""Hot"": {
              ""High"": 60,
              ""Low"": 20
          }
      }
    }
    ";
        // Create a JsonNode DOM from a JSON string.
        JsonNode forecastNode = JsonNode.Parse(jsonString)!;
        var value = forecastNode["TemperatureRanges"]["Hot"]["High"].ToString();
        Assert.AreEqual("60", value);
        var date = forecastNode["DatesAvailable"][0].GetValue<string>();
        Assert.AreEqual("2019-08-01T00:00:00", date);
    }

    [Test]
    public void ChangePropNames()
    {
        var json = @"{
  ""id"": ""b9d7574f-5246-4c94-ade5-1d4e9b169afc"",
  ""name"": ""James-John Moonly-Batcher"",
  ""monthly-amount"" : 2000,
  ""annual-amount"" : 12000,
  ""ask-for-shiping-address"" : false
}";
        var jsonObject = JsonNode.Parse(json).AsObject();
        var toModify = jsonObject.Where(pair => pair.Key.Contains("-")).ToList();
        foreach (var prop in toModify)
        {
            var name = prop.Key;
            var value = prop.Value;
            var newName = name.Replace("-", "");
            jsonObject.Remove(name);
            jsonObject[newName] = value;
        }

        Assert.That(jsonObject.ToJsonString(),
            Is.EqualTo(
                @"{""id"":""b9d7574f-5246-4c94-ade5-1d4e9b169afc"",""name"":""James-John Moonly-Batcher"",""monthlyamount"":2000,""annualamount"":12000,""askforshipingaddress"":false}"));

    }
}