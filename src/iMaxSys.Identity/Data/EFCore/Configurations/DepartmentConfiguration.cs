//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: DepartmentConfiguration.cs
//摘要: Department映射配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using iMaxSys.Max.Data.EFCore.Configurations;
using iMaxSys.Identity.Data.Models;

namespace iMaxSys.Identity.Data.EFCore.Configurations
{
    /// <summary>
    /// Department映射配置
    /// </summary>
    public class DepartmentConfiguration : TenantMasterEntityConfiguration<Department>
    {
        protected override void Configures(EntityTypeBuilder<Department> builder)
        {
            //基类配置
            base.Configures(builder);
            //名称
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            //别名
            builder.Property(x => x.Alias).HasColumnName("alias").HasMaxLength(50).IsRequired();
            //描述
            builder.Property(x => x.Descripton).HasColumnName("descripton").HasMaxLength(255);
            //状态
            builder.Property(x => x.Status).HasColumnName("status").IsRequired();
            //ToTable
            builder.ToTable("department");
        }
    }
}


