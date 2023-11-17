using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET8WebApiTests.Controllers;

[ApiController]
[ApiVersion("1.0")]
// [Route("/api/[controller]")]
[Route("v{version:apiVersion}/api/[controller]")]
public class HomeApiController : ControllerBase
{
    private readonly ILogger<HomeApiController> _logger;

    public HomeApiController(ILogger<HomeApiController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get() => Ok(new { Test = "Ok"});
}