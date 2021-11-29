using Microsoft.AspNetCore.Mvc;

namespace Kylin.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public string Submit()
        {
            return $"this is a order from {DateTime.Now}";
        }
    }
}