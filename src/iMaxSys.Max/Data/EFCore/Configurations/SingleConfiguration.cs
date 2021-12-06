//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: SingleEntityConfiguration.cs
//摘要: SingleEntityConfiguration
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Data.Entities;

namespace iMaxSys.Max.Data.EFCore.Configurations;

public abstract class SingleEntityConfiguration<T> : EntityConfiguration<T> where T : SingleEntity
{
    protected override void Configures(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.CreateTime).HasColumnName("create_time").IsRequired();
    }
}