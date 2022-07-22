﻿
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

using AutoMapper;
using iMaxSys.Identity.Models;
using Kylin.Api.ViewModels;

namespace iMaxSys.Identity.Mappers;

/// <summary>
/// 映射配置
/// </summary>
public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<MenuModel, MenuView>();
        CreateMap<OperationModel, OperationView>();
    }
}
