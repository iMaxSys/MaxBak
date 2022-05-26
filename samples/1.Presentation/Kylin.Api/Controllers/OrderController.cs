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
    }
}