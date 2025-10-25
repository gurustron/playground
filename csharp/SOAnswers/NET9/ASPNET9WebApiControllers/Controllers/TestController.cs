using Microsoft.AspNetCore.Mvc;

namespace ASPNET9WebApiControllers.Controllers;
[ApiController]
[Route("[controller]")]
public class TestController: ControllerBase
{
    private readonly WeatherForecastController _forecastController;

    public TestController(WeatherForecastController forecastController)
    {
        _forecastController = forecastController;
    }
    
    [HttpGet(Name = "Test")]
    public int Get()
    {
        return 42;
    }
}