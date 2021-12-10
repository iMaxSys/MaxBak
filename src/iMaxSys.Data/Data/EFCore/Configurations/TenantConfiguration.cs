﻿//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: TenantConfiguration.cs
//摘要: TenantConfiguration
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

using iMaxSys.Data.Models;

namespace iMaxSys.Data.EFCore.Configurations
{
    /// <summary>
    /// Tenant映射配置
    /// </summary>
    public class TenantConfiguration : MasterEntityConfiguration<Tenant>
    {
        protected override void Configures(EntityTypeBuilder<Tenant> builder)
        {
            //基类配置
            base.Configures(builder);
            //Name
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            //Alias
            builder.Property(x => x.Alias).HasColumnName("alias").HasMaxLength(50).IsRequired();
            //QuickCode
            builder.Property(x => x.QuickCode).HasColumnName("quick_code").HasMaxLength(50).IsRequired();
            //Description
            builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(255);
            //Start
            builder.Property(x => x.Start).HasColumnName("start").IsRequired();
            //End
            builder.Property(x => x.End).HasColumnName("end").IsRequired();
            //Status
            builder.Property(x => x.Status).HasColumnName("status").IsRequired();
            //ToTable
            builder.ToTable("tenant");
        }
    }
}


