﻿//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: TenantEntityConfiguration.cs
//摘要: TenantEntityConfiguration
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Data.Entities;

namespace iMaxSys.Data.EFCore.Configurations;

public abstract class TenantEntityConfiguration<T> : EntityConfiguration<T> where T : TenantEntity
{
    protected override void Configures(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.TenantId).HasColumnName("tenant_id").IsRequired();
        //索引
        builder.HasIndex(x => new { x.TenantId });
    }
}