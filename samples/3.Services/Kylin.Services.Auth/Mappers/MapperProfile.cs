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

using AutoMapper;

using Kylin.Services.Auth.Models;
using DbCustomer = Kylin.Data.Models.Auth.Customer;

namespace Kylin.Services.Auth.Mappers;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
        CreateMap<DbCustomer, Customer>();
    }
}