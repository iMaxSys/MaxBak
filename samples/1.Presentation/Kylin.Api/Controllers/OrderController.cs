using iMaxSys.Identity;
using iMaxSys.Max.Collection;
using iMaxSys.Max.Collection.Trees;
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
            /*
            LrTree tree = new(0, "earth");

            LrTreeNode treeNode = new(100, "china");
            tree.Nodes.Add(treeNode);

            treeNode = new(101, "usa");
            tree.Nodes.Add(treeNode);
            treeNode.Nodes.Add(new LrTreeNode(1010, "califonia"));
            treeNode.Nodes.Add(new LrTreeNode(1011, "fuginia"));
            treeNode.Nodes.Add(new LrTreeNode(1012, "washton"));

            treeNode = new(102, "russia");
            tree.Nodes.Add(treeNode);

            treeNode.Nodes.Add(new LrTreeNode(1020, "mosco"));
            treeNode.Nodes.Add(new LrTreeNode(1021, "holypeter"));
            treeNode.Nodes.Add(new LrTreeNode(1022, "kuzik"));

            treeNode = new(103, "uk");
            tree.Nodes.Add(treeNode);

            return tree.ToJson();
            */



            Tree tree = new(0, "earth");
            tree.Nodes.Add(new(100, "china"));
            tree.Nodes.Add(new(101, "usa"));
            tree.Nodes.Add(new(102, "russia"));

            tree.Nodes[0].Nodes.Add(new(1001, "yn"));
            tree.Nodes[0].Nodes.Add(new(1002, "sc"));
            tree.Nodes[0].Nodes.Add(new(1003, "bj"));

            tree.Nodes[1].Nodes.Add(new(1011, "ny"));
            tree.Nodes[1].Nodes.Add(new(1012, "dc"));
            tree.Nodes[1].Nodes.Add(new(1013, "wf"));

            TreeNode tn = new(102, "uk");
            tn.Nodes.Add(new TreeNode(1021, "london"));
            tn.Nodes.Add(new TreeNode(1022, "livepool"));

            tree.Nodes.Add(tn);

            return tree.ToJson();
        }
    }
}