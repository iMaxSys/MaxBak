//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: MenuConfiguration.cs
//摘要: 菜单配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Identity.Data.Entities;
using iMaxSys.Data.Repositories.EFCore.Configurations;

namespace iMaxSys.Identity.Data.EFCore.Configurations;

/// <summary>
/// Menu映射配置
/// </summary>
public class MenuConfiguration : TenantMasterEntityConfiguration<Menu>
{
    protected override void Configures(EntityTypeBuilder<Menu> builder)
    {
        //基类配置
        base.Configures(builder);
        //名称
        builder.Property(x => x.XppId).HasColumnName("xpp_id").IsRequired();
        //名称
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        //别名
        builder.Property(x => x.Alias).HasColumnName("alias").HasMaxLength(50).IsRequired();
        //Code
        builder.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
        //Code
        builder.Property(x => x.QuickCode).HasColumnName("quick_code").HasMaxLength(50).IsRequired();
        //Description
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(50);
        //Icon
        builder.Property(x => x.Icon).HasColumnName("icon").HasMaxLength(50);
        //Style
        builder.Property(x => x.Style).HasColumnName("style").HasMaxLength(50);
        //Router
        builder.Property(x => x.Router).HasColumnName("router").HasMaxLength(50);
        //Router
        builder.Property(x => x.Lv).HasColumnName("lv").IsRequired();
        //Router
        builder.Property(x => x.Rv).HasColumnName("rv").IsRequired();
        //Router
        builder.Property(x => x.Deep).HasColumnName("deep").IsRequired();
        //Status
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //Index
        builder.HasIndex(x => new { x.TenantId, x.XppId });
        //ToTable
        builder.ToTable("menu");
    }
}