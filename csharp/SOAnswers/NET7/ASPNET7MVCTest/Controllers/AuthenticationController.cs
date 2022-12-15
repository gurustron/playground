using Microsoft.AspNetCore.Mvc;

namespace ASPNET7MVCTest.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
// [ApiController]
// [Route("api/authentication")]
public class AuthenticationController : Controller
{
    [HttpPost]
    public IActionResult Test([FromForm] string toto, [FromForm] string titi)
    {
        return Ok(toto + titi);
    }
}