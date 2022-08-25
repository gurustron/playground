using System;
using Microsoft.AspNetCore.Mvc;

namespace ignite_k8s_asp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        
        [HttpGet]
        public string Get()
        {
            TryValidateModel(new object());
            return Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
        }
    }
}