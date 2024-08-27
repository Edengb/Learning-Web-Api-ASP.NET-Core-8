using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggingAndMiddlewareDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingMiddlewareController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Operation...");
        }
        
        [HttpGet]
        [Route("especial-middleware")]
        public ActionResult GetEspecialMiddleware(string teste)
        {
            return Ok("another operation...");
        }

    }
}
