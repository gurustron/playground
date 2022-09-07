using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Nodes;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET6Test.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get([FromServices] IMapper mapper)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPatch]
    public Test Patch([FromBody] Dictionary<string, object> data, 
        [FromServices] IMapper mapper)
    {
        var existing = new Test();

        var map = mapper.Map(data, existing);

        return map;
    }
    
    
    // [HttpPatch]
    // public Test Patch2(
    //     // [FromBody] ExpandoObject data,
    //     [FromBody] JsonMergePatchDocument<Test> patch,
    //     [FromServices] IMapper mapper)
    // {
    //     var test = new Test
    //     {
    //         Name = "original",
    //         Description = "Description"
    //     };
    //
    //     // var map = mapper.Map(data, test);
    //     var map = patch.ApplyTo(test);
    //     return map;
    // }
}