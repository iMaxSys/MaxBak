//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: KylinEntityConfiguration.cs
//摘要: KylinEntityConfiguration配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using iMaxSys.Data.EFCore.Configurations;
using Kylin.Data.Models.Entities;

namespace Kylin.Data.EFCore.EntityConfigurations.Entities;

/// <summary>
/// KylinMasterEntity配置
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class KylinEntityConfiguration<T> : EntityConfiguration<T> where T : KylinEntity
{
}
