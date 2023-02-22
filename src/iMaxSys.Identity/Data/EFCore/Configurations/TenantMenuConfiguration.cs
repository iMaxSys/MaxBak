//----------------------------------------------------------------
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
/// TenantMenu映射配置
/// </summary>
public class TenantMenuConfiguration : TenantMasterEntityConfiguration<TenantMenu>
{
    protected override void Configures(EntityTypeBuilder<TenantMenu> builder)
    {
        //基类配置
        base.Configures(builder);
        //MenuId
        builder.Property(x => x.MenuId).HasColumnName("menu_id").IsRequired();
        //是否可见
        builder.Property(x => x.IsShow).HasColumnName("is_show").IsRequired();
        //Status
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //Menu
        builder.HasOne(x => x.Menu).WithMany(x => x.TenantMenus).HasForeignKey(f => f.MenuId);
        //ToTable
        builder.ToTable("tenant_menu");
    }
}