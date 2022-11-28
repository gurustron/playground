using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP6MVCtest.Models;

namespace ASP6MVCtest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpGet("[controller]/search/{thing}")] 
    public string Search(int thing)
    {
        return $"The thing was {thing}";
    }
}