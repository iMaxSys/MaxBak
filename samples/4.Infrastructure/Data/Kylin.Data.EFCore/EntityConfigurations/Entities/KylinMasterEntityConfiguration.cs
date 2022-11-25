//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: KylinMasterEntityConfigurations.cs
//摘要: KylinMasterEntity配置
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
public abstract class KylinMasterEntityConfiguration<T> : MasterEntityConfiguration<T> where T : KylinMasterEntity
{
    protected override void Configures(EntityTypeBuilder<T> builder)
    {
        base.Configures(builder);
        builder.Property(x => x.GroupId).HasColumnName("group_id").IsRequired();
        builder.Property(x => x.CompanyId).HasColumnName("company_id").IsRequired();
        builder.Property(x => x.StoreId).HasColumnName("store_id").IsRequired();
        builder.HasIndex(x => new { x.GroupId, x.CompanyId, x.StoreId });
    }
}
