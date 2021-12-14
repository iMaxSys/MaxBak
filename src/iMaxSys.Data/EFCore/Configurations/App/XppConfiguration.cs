﻿//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: XappConfiguration.cs
//摘要: 应用配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using iMaxSys.Data.EFCore.Configurations;
using iMaxSys.Data.Entities.App;

namespace iMaxSys.Data.EFCore.Configurations.App;

/// <summary>
/// 应用映射配置
/// </summary>
public class XappConfiguration : MasterEntityConfiguration<Xpp>
{
    protected override void Configures(EntityTypeBuilder<Xpp> builder)
    {
        //基类配置
        base.Configures(builder);
        //Name
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        //Alias
        builder.Property(x => x.Alias).HasColumnName("alias").HasMaxLength(50).IsRequired();
        //Description
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(255);
        //AppSource
        builder.Property(x => x.Source).HasColumnName("source").IsRequired(); ;
        //第三方平台原始Id(暂不使用)
        builder.Property(x => x.AccountId).HasColumnName("account_id").HasMaxLength(50);
        //AppId
        builder.Property(x => x.AppId).HasColumnName("app_id").HasMaxLength(50);
        //AppKey
        builder.Property(x => x.AppKey).HasColumnName("app_key").HasMaxLength(50);
        //状态
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //ToTable
        builder.ToTable("xpp");
    }
}