//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: TenantSingleEntityConfiguration.cs
//摘要: TenantSingleEntityConfiguration
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Data.Entities;

namespace iMaxSys.Max.Data.EFCore.Configurations;

public abstract class TenantSingleEntityConfiguration<T> : SingleEntityConfiguration<T> where T : TenantSingleEntity
{
    protected override void Configures(EntityTypeBuilder<T> builder)
    {
        base.Configures(builder);
        builder.Property(x => x.TenantId).HasColumnName("tenant_id").IsRequired();
        //索引
        builder.HasIndex(x => new { x.TenantId });
    }
}