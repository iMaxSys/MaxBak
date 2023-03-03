//----------------------------------------------------------------
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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using iMaxSys.Core.Data.Entities;
using iMaxSys.Data.EFCore.Configurations;

namespace iMaxSys.Core.Data.EFCore.Configurations;

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
        //Code
        builder.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
        //QuickCode
        builder.Property(x => x.QuickCode).HasColumnName("quick_code").HasMaxLength(50).IsRequired();
        //Description
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(255);
        //Description
        builder.Property(x => x.License).HasColumnName("license").HasMaxLength(50);
        //Level
        builder.Property(x => x.Level).HasColumnName("level").IsRequired();
        //Logo
        builder.Property(x => x.Logo).HasColumnName("logo").HasMaxLength(255);
        //Contact
        builder.Property(x => x.Contact).HasColumnName("contact").HasMaxLength(50);
        //Phone
        builder.Property(x => x.Phone).HasColumnName("phone").HasMaxLength(50);
        //Mail
        builder.Property(x => x.Mail).HasColumnName("mail").HasMaxLength(50);
        //Address
        builder.Property(x => x.Address).HasColumnName("address").HasMaxLength(255);
        //Start
        builder.Property(x => x.Start).HasColumnName("start").IsRequired();
        //End
        builder.Property(x => x.End).HasColumnName("end").IsRequired();
        //Status
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //Index
        builder.HasIndex(x => new { x.Name, x.Code });
        //ToTable
        builder.ToTable("tenant");
    }
}