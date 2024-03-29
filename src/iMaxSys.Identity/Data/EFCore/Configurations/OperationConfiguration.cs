﻿//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: MemberConfiguration.cs
//摘要: 会员配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Identity.Data.Entities;
using iMaxSys.Data.EFCore.Configurations;

namespace iMaxSys.Identity.Data.EFCore.Configurations;

/// <summary>
/// Operation映射配置
/// </summary>
public class OperationConfiguration : TenantMasterEntityConfiguration<Operation>
{
    protected override void Configures(EntityTypeBuilder<Operation> builder)
    {
        //基类配置
        base.Configures(builder);
        //名称
        builder.Property(x => x.XppId).HasColumnName("xpp_id").IsRequired();
        //MenuId
        builder.Property(x => x.MenuId).HasColumnName("menu_id").IsRequired();
        //名称
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        //别名
        builder.Property(x => x.Alias).HasColumnName("alias").HasMaxLength(50).IsRequired();
        //Code
        builder.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
        //Code
        builder.Property(x => x.QuickCode).HasColumnName("quick_code").HasMaxLength(50).IsRequired();
        //Descripton
        builder.Property(x => x.Descripton).HasColumnName("descripton").HasMaxLength(50);
        //Descripton
        builder.Property(x => x.Value).HasColumnName("value").HasMaxLength(50);
        //Icon
        builder.Property(x => x.Icon).HasColumnName("icon").HasMaxLength(50);
        //Style
        builder.Property(x => x.Style).HasColumnName("style").HasMaxLength(50);
        //Type
        builder.Property(x => x.Type).HasColumnName("type").IsRequired();
        //ServerRouter
        builder.Property(x => x.ServerRouter).HasColumnName("server_router").HasMaxLength(50).HasComment("ServerRouter");
        //ClientRouter
        builder.Property(x => x.ClientRouter).HasColumnName("client_router").HasMaxLength(50).HasComment("ClientRouter");
        //是否可见
        builder.Property(x => x.IsShow).HasColumnName("is_show").IsRequired();
        //Status
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //Menu
        builder.HasOne(x => x.Menu).WithMany(x => x.Operations).HasForeignKey(f => f.MenuId);
        //索引
        builder.HasIndex(x => new { x.TenantId, x.XppId });
        //ToTable
        builder.ToTable("operation");
    }
}