using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kylin.Api.Client.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly MaxOption _maxOption;

        public AuthController(IOptions<MaxOption> option)
        {
            _maxOption = option.Value;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //throw new MaxException(-10, "");
            return View();
        }

        [HttpGet]
        public object Name()
        {
            return _maxOption;
            //throw new MaxException(99, "错误");
            //return "hello world";
        }
    }
}

