using System.Globalization;
using LocalizationTest.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LocalizationTest.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController(IStringLocalizer<SharedResource> localizer) : ControllerBase
{
    [HttpGet("/testloc")]
    public IActionResult TestLoc()
    {
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUICulture = CultureInfo.CurrentUICulture;

        var forcedCulture = new CultureInfo("en");
        CultureInfo.CurrentCulture = forcedCulture;
        CultureInfo.CurrentUICulture = forcedCulture;

        var testKey = "Title";
        var valueForEn = localizer[testKey];

        CultureInfo.CurrentCulture = originalCulture;
        CultureInfo.CurrentUICulture = originalUICulture;

        var allResourceNames = typeof(SharedResource).Assembly
            .GetManifestResourceNames();

        return Ok(new
        {
            ForcedCulture = "en",
            Value = valueForEn,
            ResourceNamesInAssembly = allResourceNames
        });
    }
}