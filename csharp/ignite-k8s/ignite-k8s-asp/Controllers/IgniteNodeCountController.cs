using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ignite_k8s_asp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IgniteNodeCountController : ControllerBase
    {
       private readonly ILogger<IgniteNodeCountController> _logger;

        public IgniteNodeCountController(ILogger<IgniteNodeCountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Get()
        {
            return Program.Ignite.GetCluster().GetNodes().Count;
        }
    }
}