using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET7MVCTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] Jerry value)
    {
        var a = value;
        return Ok(value);
    }
    
    public enum SampleEnum
    {
        Bar,
        Baz,
    }
    public class Jerry
    {
        [JsonConverter(typeof(CustomEnumConverter<SampleEnum>))]
        public SampleEnum MyEnum { get; set; }
    }

class CustomEnumConverter<T>:JsonConverter<T>  where T: struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (Enum.TryParse<T>(reader.GetString(), out var r))
        {
            return r;
        }
        return default;
    }
    
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString("G"));
}
}

