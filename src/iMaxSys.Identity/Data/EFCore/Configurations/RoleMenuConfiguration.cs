//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: RoleMenuConfiguration.cs
//摘要: RoleMenu
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
/// RoleMenu映射配置
/// </summary>
public class RoleMenuConfiguration : TenantMasterEntityConfiguration<RoleMenu>
{
    protected override void Configures(EntityTypeBuilder<RoleMenu> builder)
    {
        //基类配置
        base.Configures(builder);
        //RoleId
        builder.Property(x => x.RoleId).HasColumnName("role_id").IsRequired();
        //MemberId
        builder.Property(x => x.MenuId).HasColumnName("menu_id").IsRequired();
        //XppId
        builder.Property(x => x.XppId).HasColumnName("xpp_id").IsRequired();
        //Status
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //关系
        builder.HasOne(x => x.Role).WithMany(x => x.RoleMenus).HasForeignKey(x => x.RoleId);
        //关系
        builder.HasOne(x => x.Menu).WithMany(x => x.RoleMenus).HasForeignKey(x => x.MenuId);
        //Index
        builder.HasIndex(x => new { x.TenantId, x.RoleId, x.MenuId, x.XppId });
        //ToTable
        builder.ToTable("role_menu");
    }
}