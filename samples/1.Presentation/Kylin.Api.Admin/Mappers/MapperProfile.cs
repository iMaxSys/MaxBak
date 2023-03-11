//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AuthMapperProfile.cs
//摘要: AuthMapperProfile
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max;
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Collection;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Identity.Models;

using Kylin.Api.Admin.ViewModels;

namespace Kylin.Api.Admin.Mappers;

/// <summary>
/// MapperProfile
/// </summary>
public class MapperProfile : Profile
{
    /// <summary>
    /// MapperProfile
    /// </summary>
    public MapperProfile()
    {
        CreateMap<Tenant, TenantApiResponse>();
        CreateMap<LoginResult, LoginApiResponse>().ForMember(t => t.Expires, opt => opt.MapFrom(s => s.Expires.ToNormalString()));
        CreateMap<RoleResult, RoleApiResponse>()
            .ForMember(t => t.Start, opt => opt.MapFrom(s => s.Start.ToDateString()))
            .ForMember(t => t.End, opt => opt.MapFrom(s => s.End.ToDateString()));
        CreateMap<Role, RoleApiResponse>();
        CreateMap<MemberResult, MemberApiResponse>().ForMember(t => t.Role, opt => opt.MapFrom(s => s.Roles!.FirstOrDefault()));
        CreateMap<Department, DepartmentApiResponse>();

        CreateMap<PagedList<RoleResult>, PagedList<RoleApiResponse>>()
               .ConstructUsing((s, t) => { return new PagedList<RoleApiResponse>(t.Mapper.Map<List<RoleApiResponse>>(s.Items), s.Index, s.Size, s.Total); });

        CreateMap<UpdateRoleApiRequest, UpdateRoleRequest>();
        CreateMap<AddRoleApiRequest, AddRoleRequest>();
        CreateMap<MenuIdsResult, MenuIdsApiResponse>();
    }
}