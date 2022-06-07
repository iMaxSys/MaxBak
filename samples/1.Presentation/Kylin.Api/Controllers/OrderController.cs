using iMaxSys.Identity;
using iMaxSys.Max.Collection;
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
        public string ToJson()
        {

            Tree tree = new(0, "地球");

            TreeNode treeNode = new(100, "中国");
            tree.Nodes.Add(treeNode);

            treeNode = new(101, "美国");
            tree.Nodes.Add(treeNode);

            treeNode = new(102, "俄罗斯");
            tree.Nodes.Add(treeNode);

            return tree.ToJson();
        }
    }
}