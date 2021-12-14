
//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MasterEntityConfiguration.cs
//摘要: MasterEntityConfiguration
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Data.Entities;

namespace iMaxSys.Data.EFCore.Configurations;

public abstract class MasterEntityConfiguration<T> : SingleEntityConfiguration<T> where T : MasterEntity
{
    protected override void Configures(EntityTypeBuilder<T> builder)
    {
        base.Configures(builder);
        builder.Property(x => x.CreatorId).HasColumnName("creator_id").IsRequired();
        builder.Property(x => x.Creator).HasColumnName("creator").IsRequired().HasMaxLength(50);
        builder.Property(x => x.ReviserId).HasColumnName("reviser_id").IsRequired();
        builder.Property(x => x.Reviser).HasColumnName("reviser").IsRequired().HasMaxLength(50);
        builder.Property(x => x.ReviseTime).HasColumnName("revise_time").IsRequired();
    }
}
