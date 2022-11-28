using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;

namespace SOAnswers.Tests.JsonTests.SystemText;

public class DeserTests
{
    [Test]
    public void Do()
    {
        var json = @"{""title"":""Username or password is incorrect"",""errors"":[]}";
        var errorDto = JsonSerializer.Deserialize<ErrorDto>(json);
    }

    public class ErrorItem
    {
        public string Message { get; set; }
        public string Tag { get; set; }
    }

    public class ErrorDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        public List<ErrorItem> Errors { get; set; } = new();
    }
}