using iMaxSys.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kylin.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly ICheckCodeService _checkCodeService;

        public OrderController(ICheckCodeService checkCodeService)
        {
            _checkCodeService = checkCodeService;
        }
        [HttpGet]
        public async Task<string> Submit()
        {
            string result = await _checkCodeService.Get();
            return $"this is a order from {result}";
        }
    }
}