using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SOAnswers.Tests.JsonTests.NewtonsoftJson;

public class Tests
{
    [Test]
    public void Do()
    {
        var deserializeObject = JsonConvert.DeserializeObject<Dictionary<string,MyClass>>(@"
{
    ""afghanistan"":{
    ""iso"":{
      ""af"":1
    }
}
}
");
        var s = deserializeObject.First().Value.ToString();
        deserializeObject.First().Value.Iso.Keys.FirstOrDefault();
        var jObject = JObject.Parse(@"{
    ""token"": {
        ""accessToken"": ""scrciFyGuLAQn6XgKkaBWOxdZA1"",
        ""issuedAt"": ""2022-11-06T22:54:27Z"",
        ""expiresIn"": 1799
    }
}");
        var token = jObject["token"].ToObject<Token>();
    }
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime IssuedAt { get; set; }
        public int ExpiresIn { get; set; }
    }

    class MyClass
    {
        public Dictionary<string, int> Iso { get; set; }
        
    }
}