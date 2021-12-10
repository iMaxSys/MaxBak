
using System.Linq;

using AutoMapper;

using iMaxSys.Max.Extentions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Algorithm.Collection;

using DbRole = iMaxSys.Identity.Data.Models.Role;
using DbMenu = iMaxSys.Identity.Data.Models.Menu;
using DbOperation = iMaxSys.Identity.Data.Models.Operation;
using DbTenant = iMaxSys.Data.Models.Tenant;
using DbMember = iMaxSys.Identity.Data.Models.Member;

namespace iMaxSys.Identity.Data.Mappers
{
    public class IdentityMapperProfile : Profile
    {
        public IdentityMapperProfile()
        {
            CreateMap<DbTenant, Tenant>();

            CreateMap<DbRole, IRole>()
                .ForMember(t => t.MenuIds, opt => opt.MapFrom(s => s.MenuIds == null ? null : (s.MenuIds == "*" ? new long[] { 0 } : s.MenuIds.ToLongArray())))
                .ForMember(t => t.OperationIds, opt => opt.MapFrom(s => s.OperationIds == null ? null : (s.OperationIds == "*" ? new long[] { 0 } : s.OperationIds.ToLongArray())));

            CreateMap<DbMenu, IMenu>();

            CreateMap<DbMember, IMember>()
                .ForMember(t => t.LoginName, opt => opt.MapFrom(s => s.LoginName ?? s.Mobile))
                .ForMember(t => t.Name, opt => opt.MapFrom(s => s.Name ?? s.NickName));

            CreateMap<DbOperation, IOperation>();

            CreateMap<MenuShadow, IMenu>();

            //CreateMap<DbMenu, TreeStore>()
            //    .ForMember(t => t.Action, opt => opt.MapFrom(s => s.Router))
            //    .ForMember(t => t.Ext, opt => opt.MapFrom(s => string.Join(",", s.Operations.Select(x => x.Router))));

            //CreateMap<TreeNode, IMenu>()
            //    .ForMember(t => t.Router, opt => opt.MapFrom(s => s.Action))
            //    .ForMember(t => t.Operations, opt => opt.MapFrom(s => s.Ext))
            //    .ForMember(t => t.Menus, opt => opt.MapFrom(s => s.Nodes));
        }
    }
}
