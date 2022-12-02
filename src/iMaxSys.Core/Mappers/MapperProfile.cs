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
//日期：2022-11-15
//----------------------------------------------------------------

using AutoMapper;

using iMaxSys.Core.Models;
using iMaxSys.Core.Data.Entities;

namespace iMaxSys.Core.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Xpp, XppModel>();
        CreateMap<XppSns, XppSnsModel>();
        CreateMap<Dict, DictModel>();
        CreateMap<DictItem, DictItemModel>();
    }
}