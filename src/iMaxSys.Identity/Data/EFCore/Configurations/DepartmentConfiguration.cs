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

using iMaxSys.Identity.Data.Entities;
using iMaxSys.Data.EFCore.Configurations;

namespace iMaxSys.Identity.Data.EFCore.Configurations;

/// <summary>
/// Department映射配置
/// </summary>
public class DepartmentConfiguration : TenantMasterEntityConfiguration<Department>
{
    protected override void Configures(EntityTypeBuilder<Department> builder)
    {
        //基类配置
        base.Configures(builder);
        //XppId
        builder.Property(x => x.XppId).HasColumnName("xpp_id").IsRequired();
        //父节点id
        builder.Property(x => x.ParentId).HasColumnName("parent_id").IsRequired(false).HasComment("父节点id");
        //名称
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired().HasComment("名称");
        //左值
        builder.Property(x => x.Lv).HasColumnName("lv").IsRequired().HasComment("左值");
        //右值
        builder.Property(x => x.Rv).HasColumnName("rv").IsRequired().HasComment("右值");
        //序号
        builder.Property(x => x.Index).HasColumnName("index").IsRequired().HasComment("索引");
        //深度
        builder.Property(x => x.Level).HasColumnName("level").IsRequired().HasComment("深度");
        //是否根点
        builder.Property(x => x.IsRoot).HasColumnName("is_root").IsRequired().HasComment("是否根点");
        //是否叶节点
        builder.Property(x => x.IsLeaf).HasColumnName("is_leaf").IsRequired().HasComment("是否叶节点");
        //类型
        builder.Property(x => x.Type).HasColumnName("type").IsRequired().HasComment("类型");
        //Code
        builder.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).HasComment("Code");
        //QuickCode
        builder.Property(x => x.QuickCode).HasColumnName("quick_code").HasMaxLength(50).HasComment("QuickCode");
        //值
        builder.Property(x => x.Value).HasColumnName("value").HasMaxLength(50).HasComment("值");
        //别名
        builder.Property(x => x.Alias).HasColumnName("alias").HasMaxLength(50).HasComment("是否叶节点");
        //描述
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(255).HasComment("描述");
        //Style
        builder.Property(x => x.Style).HasColumnName("style").HasMaxLength(50).HasComment("Style");
        //SelectedStyle
        builder.Property(x => x.SelectedStyle).HasColumnName("selected_style").HasMaxLength(50).HasComment("SelectedStyle");
        //Icon
        builder.Property(x => x.Icon).HasColumnName("icon").HasMaxLength(50).HasComment("icon");
        //SelectedIcon
        builder.Property(x => x.SelectedIcon).HasColumnName("selected_icon").HasMaxLength(50).HasComment("SelectedIcon");
        //Ext
        builder.Property(x => x.Ext).HasColumnName("ext").HasMaxLength(255).HasComment("Ext");
        //状态
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //关系
        builder.HasOne(x => x.Parent).WithMany(x => x.Departments).HasForeignKey(x => x.ParentId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        //索引
        builder.HasIndex(x => new { x.TenantId, x.Name });
        //索引
        builder.HasIndex(x => new { x.TenantId, x.QuickCode });
        //ToTable
        builder.ToTable("department");
    }
}