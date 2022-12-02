//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MapperProfiles.cs
//摘要: 映射配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-07-07
//----------------------------------------------------------------

using iMaxSys.Max.Extentions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Identity.Models;

using DbRole = iMaxSys.Identity.Data.Entities.Role;
using DbMenu = iMaxSys.Identity.Data.Entities.Menu;
using DbMember = iMaxSys.Identity.Data.Entities.Member;
using DbOperation = iMaxSys.Identity.Data.Entities.Operation;
using DbDepartment = iMaxSys.Identity.Data.Entities.Department;

namespace iMaxSys.Identity.Mappers
{
    /// <summary>
    /// 映射配置
    /// </summary>
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DbMember, MemberModel>();
            CreateMap<MemberModel, DbMember>();

            CreateMap<DbRole, RoleModel>()
                .ForMember(t => t.MenuIds, opt => opt.MapFrom(s => s.MenuIds == null ? null : (s.MenuIds == "0" ? new long[] { 0 } : s.MenuIds.ToLongArray())))
                .ForMember(t => t.OperationIds, opt => opt.MapFrom(s => s.OperationIds == null ? null : (s.OperationIds == "0" ? new long[] { 0 } : s.OperationIds.ToLongArray())));

            CreateMap<RoleModel, DbRole>()
                .ForMember(t => t.MenuIds, opt => opt.MapFrom(s => s.MenuIds == null ? null : (s.MenuIds[0] == 0 ? "0" : string.Join(",", s.MenuIds))))
                .ForMember(t => t.OperationIds, opt => opt.MapFrom(s => s.OperationIds == null ? null : (s.OperationIds[0] == 0 ? "0" : string.Join(",", s.OperationIds))));

            CreateMap<DbDepartment, Department>();
            CreateMap<DbDepartment, IDepartment>().As<Department>();
            CreateMap<DepartmentModel, Department>();
            CreateMap<Department, DepartmentModel>();
            CreateMap<MenuModel, DbMenu>();
            CreateMap<DbMenu, MenuModel>();
            CreateMap<DbOperation, OperationModel>();
            CreateMap<OperationModel, DbOperation>();
            CreateMap<DbOperation, IOperation>().As<OperationModel>();

            CreateMap<ITree<DbMenu>, IMenu>()
               .ForMember(t => t.Action, opt => opt.MapFrom(s => s.Data.Action))
               .ForMember(t => t.Alias, opt => opt.MapFrom(s => s.Data.Alias))
               .ForMember(t => t.Code, opt => opt.MapFrom(s => s.Data.Code))
               .ForMember(t => t.Description, opt => opt.MapFrom(s => s.Data.Description))
               .ForMember(t => t.Ext, opt => opt.MapFrom(s => s.Data.Ext))
               .ForMember(t => t.Icon, opt => opt.MapFrom(s => s.Data.Icon))
               .ForMember(t => t.Id, opt => opt.MapFrom(s => s.Data.Id))
               .ForMember(t => t.Index, opt => opt.MapFrom(s => s.Data.Index))
               .ForMember(t => t.IsLeaf, opt => opt.MapFrom(s => s.Data.IsLeaf))
               .ForMember(t => t.IsRoot, opt => opt.MapFrom(s => s.Data.IsRoot))
               .ForMember(t => t.IsShow, opt => opt.MapFrom(s => s.Data.IsShow))
               .ForMember(t => t.Level, opt => opt.MapFrom(s => s.Data.Level))
               .ForMember(t => t.Lv, opt => opt.MapFrom(s => s.Data.Lv))
               .ForMember(t => t.Name, opt => opt.MapFrom(s => s.Data.Name))
               .ForMember(t => t.ParentId, opt => opt.MapFrom(s => s.Data.ParentId))
               .ForMember(t => t.QuickCode, opt => opt.MapFrom(s => s.Data.QuickCode))
               .ForMember(t => t.Rv, opt => opt.MapFrom(s => s.Data.Rv))
               .ForMember(t => t.SelectedIcon, opt => opt.MapFrom(s => s.Data.SelectedIcon))
               .ForMember(t => t.SelectedStyle, opt => opt.MapFrom(s => s.Data.SelectedStyle))
               .ForMember(t => t.Status, opt => opt.MapFrom(s => s.Data.Status))
               .ForMember(t => t.Style, opt => opt.MapFrom(s => s.Data.Style))
               .ForMember(t => t.Type, opt => opt.MapFrom(s => s.Data.Type))
               .ForMember(t => t.Value, opt => opt.MapFrom(s => s.Data.Value))
               .ForMember(t => t.Operations, opt => opt.MapFrom(s => s.Data.Operations))
               .ForMember(t => t.Children, opt => opt.MapFrom(s => s.Children)).As<MenuModel>();

            CreateMap<ITree<DbMenu>, MenuModel>()
               .ForMember(t => t.Action, opt => opt.MapFrom(s => s.Data.Action))
               .ForMember(t => t.Alias, opt => opt.MapFrom(s => s.Data.Alias))
               .ForMember(t => t.Code, opt => opt.MapFrom(s => s.Data.Code))
               .ForMember(t => t.Description, opt => opt.MapFrom(s => s.Data.Description))
               .ForMember(t => t.Ext, opt => opt.MapFrom(s => s.Data.Ext))
               .ForMember(t => t.Icon, opt => opt.MapFrom(s => s.Data.Icon))
               .ForMember(t => t.Id, opt => opt.MapFrom(s => s.Data.Id))
               .ForMember(t => t.Index, opt => opt.MapFrom(s => s.Data.Index))
               .ForMember(t => t.IsLeaf, opt => opt.MapFrom(s => s.Data.IsLeaf))
               .ForMember(t => t.IsRoot, opt => opt.MapFrom(s => s.Data.IsRoot))
               .ForMember(t => t.IsShow, opt => opt.MapFrom(s => s.Data.IsShow))
               .ForMember(t => t.Level, opt => opt.MapFrom(s => s.Data.Level))
               .ForMember(t => t.Lv, opt => opt.MapFrom(s => s.Data.Lv))
               .ForMember(t => t.Name, opt => opt.MapFrom(s => s.Data.Name))
               .ForMember(t => t.ParentId, opt => opt.MapFrom(s => s.Data.ParentId))
               .ForMember(t => t.QuickCode, opt => opt.MapFrom(s => s.Data.QuickCode))
               .ForMember(t => t.Rv, opt => opt.MapFrom(s => s.Data.Rv))
               .ForMember(t => t.SelectedIcon, opt => opt.MapFrom(s => s.Data.SelectedIcon))
               .ForMember(t => t.SelectedStyle, opt => opt.MapFrom(s => s.Data.SelectedStyle))
               .ForMember(t => t.Status, opt => opt.MapFrom(s => s.Data.Status))
               .ForMember(t => t.Style, opt => opt.MapFrom(s => s.Data.Style))
               .ForMember(t => t.Type, opt => opt.MapFrom(s => s.Data.Type))
               .ForMember(t => t.Value, opt => opt.MapFrom(s => s.Data.Value))
               .ForMember(t => t.Operations, opt => opt.MapFrom(s => s.Data.Operations))
               .ForMember(t => t.Children, opt => opt.MapFrom(s => s.Children));
        }
    }
}