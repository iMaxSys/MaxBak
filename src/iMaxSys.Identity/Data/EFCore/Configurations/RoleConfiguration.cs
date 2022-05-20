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
/// Role映射配置
/// </summary>
public class RoleConfiguration : TenantMasterEntityConfiguration<Role>
{
    protected override void Configures(EntityTypeBuilder<Role> builder)
    {
        //基类配置
        base.Configures(builder);
        //XppId
        builder.Property(x => x.XppId).HasColumnName("xpp_id").IsRequired();
        //Name
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50);
        //别名
        builder.Property(x => x.Alias).HasColumnName("alias").HasMaxLength(50).IsRequired();
        //Code
        builder.Property(x => x.Code).HasColumnName("code").HasMaxLength(50);
        //QuickCode
        builder.Property(x => x.QuickCode).HasColumnName("quick_code").HasMaxLength(50);
        //Descripton
        builder.Property(x => x.Descripton).HasColumnName("descripton").HasMaxLength(50);
        //Icon
        builder.Property(x => x.Icon).HasColumnName("icon").HasMaxLength(50);
        //Style
        builder.Property(x => x.Style).HasColumnName("style").HasMaxLength(50);
        //Menus
        builder.Property(x => x.MenuIds).HasColumnName("menu_ids").HasMaxLength(5000);
        //Operations
        builder.Property(x => x.OperationIds).HasColumnName("operation_ids").HasMaxLength(5000);
        //Start
        builder.Property(x => x.Start).HasColumnName("start");
        //End
        builder.Property(x => x.End).HasColumnName("end");
        //Status
        builder.Property(x => x.Status).HasColumnName("status");
        //Index
        builder.HasIndex(x => new { x.TenantId });
        //ToTable
        builder.ToTable("role");
    }
}