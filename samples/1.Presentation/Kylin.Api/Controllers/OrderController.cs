using AutoMapper;
using iMaxSys.Identity;
using iMaxSys.Max.Collection;
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Max.Extentions;
using Kylin.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Kylin.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICheckCodeService _checkCodeService;
        private readonly IDepartmentService _departmentService;
        private readonly IMenuService _menuService;

        public OrderController(IMapper mapper, ICheckCodeService checkCodeService, IDepartmentService departmentService, IMenuService menuService)
        {
            _mapper = mapper;
            _checkCodeService = checkCodeService;
            _departmentService = departmentService;
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<object?> ToJson()
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

            //iMaxSys.Identity.Models.DepartmentModel model = new();
            //model.Name = "DC";
            //model.Code = "022";

            //await _departmentService.AddAsync(0, 0, 763824386808479744, model);

            //iMaxSys.Identity.Models.DepartmentModel model = new();
            //model.Name = "四川";
            //model.Code = "028";

            //await _departmentService.AddAsync(0, 763822752867024896, null, model);

            //model = new();
            //model.Name = "云南";
            //model.Code = "087";

            //await _departmentService.AddAsync(0, 763822752867024896, null, model);

            //model = new();
            //model.Name = "美国";
            //model.Code = "002";

            //await _departmentService.AddAsync(0, 0, null, model);

            //await _departmentService.InsertAsync();


            //iMaxSys.Identity.Models.MenuModel model1 = new();
            //model1.Name = "菜单1";
            //model1.Code = "001";

            //await _menuService.AddAsync(0, 0, null, model1);

            //iMaxSys.Identity.Models.DepartmentModel model = new();
            //model.Id = 763824390721765376;
            //model.Name = "China";
            //await _departmentService.UpdateAsync(0, model);


            //var dept = await _departmentService.GetAsync(0, 0);
            //return dept?.ToJson();


            //Tree tree = new(0, "earth");
            //tree.Nodes.Add(new(100, "china"));
            //tree.Nodes.Add(new(101, "usa"));
            //tree.Nodes.Add(new(102, "russia"));

            //tree.Nodes[0].Nodes.Add(new(1001, "yn"));
            //tree.Nodes[0].Nodes.Add(new(1002, "sc"));
            //tree.Nodes[0].Nodes.Add(new(1003, "bj"));

            //tree.Nodes[1].Nodes.Add(new(1011, "ny"));
            //tree.Nodes[1].Nodes.Add(new(1012, "dc"));
            //tree.Nodes[1].Nodes.Add(new(1013, "wf"));

            //TreeNode tn = new(102, "uk");
            //tn.Nodes.Add(new TreeNode(1021, "london"));
            //tn.Nodes.Add(new TreeNode(1022, "livepool"));

            //tree.Nodes.Add(tn);

            //return tree.ToJson();

            var menus = await _menuService.GetRoleMenuAsync(0, 0, 200);
            var menu = _mapper.Map<MenuView>(menus);
            return menus;
        }
    }
}