//----------------------------------------------------------------
//Copyright (C) 2016-2026 Care Co.,Ltd.
//All rights reserved.
//
//文件: IdentityMapperProfile.cs
//摘要: 身份系统类型映射配置
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Extentions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Algorithm.Collection;

using DbTenant = iMaxSys.Data.Entities.App.Tenant;
using DbRole = iMaxSys.Identity.Data.Entities.Role;
using DbMenu = iMaxSys.Identity.Data.Entities.Menu;
using DbOperation = iMaxSys.Identity.Data.Entities.Operation;
using DbMember = iMaxSys.Identity.Data.Entities.Member;

namespace iMaxSys.Identity.Data.Mappers;

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
            .ForMember(t => t.UserName, opt => opt.MapFrom(s => s.UserName.IfNotNull(s.Mobile)))
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