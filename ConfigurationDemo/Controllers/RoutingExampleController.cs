using Microsoft.AspNetCore.Mvc;

namespace ConfigurationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutingExampleController(ILogger<RoutingExampleController> logger) : ControllerBase
    {
        [HttpGet("{id:int:range(1, 100)}")]
        public IActionResult Get(int id, string teste)
        {
            return Ok($"Getting ID from PATH URL: {id} and teste from QUERY Parameter: {teste}");
        }
    }
}
