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

using iMaxSys.Identity.Models;
using Kylin.Api.Admin.ViewModels;

namespace Kylin.Api.Admin.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginResult, LoginResponse>();
        CreateMap<RoleModel, RoleResponse>();
        CreateMap<MemberModel, MemberResponse>().ForMember(t => t.Role, opt => opt.MapFrom(s => s.Roles!.FirstOrDefault()));
    }
}