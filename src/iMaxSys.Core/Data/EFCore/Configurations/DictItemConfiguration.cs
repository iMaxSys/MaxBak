//----------------------------------------------------------------
//Copyright (C) 2016-2048 Co.,Ltd.
//All rights reserved.
//
//文件: Configuration.cs
//摘要: 配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-11-16
//----------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using iMaxSys.Core.Data.Entities;
using iMaxSys.Data.EFCore.Configurations;                            

namespace iMaxSys.Core.Data.EFCore.Configurations;

/// <summary>
/// Dictionary映射配置
/// </summary>
public class DictDetailConfiguration : TenantMasterEntityConfiguration<DictItem>
{
    protected override void Configures(EntityTypeBuilder<DictItem> builder)
    {
        //基类配置
        base.Configures(builder);

        //名称
        builder.Property(x => x.DictId).HasColumnName("dict_id").HasComment("字典id");
        //名称
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).HasComment("名称");
        //别名
        builder.Property(x => x.Alias).HasColumnName("alias").HasMaxLength(50).HasComment("别名");
        //编号
        builder.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).HasComment("编号");
        //速查码
        builder.Property(x => x.QuickCode).HasColumnName("quick_code").HasMaxLength(50).HasComment("速查码");
        //value
        builder.Property(x => x.Value).HasColumnName("value").HasMaxLength(50).HasComment("value");
        //描述
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(255).HasComment("描述");
        //缩略图
        builder.Property(x => x.Thumbnail).HasColumnName("thumbnail").HasMaxLength(255).HasComment("缩略图");
        //图像
        builder.Property(x => x.Image).HasColumnName("image").HasMaxLength(255).HasComment("图像");
        //style
        builder.Property(x => x.Style).HasColumnName("style").HasMaxLength(50).HasComment("style");
        //序号
        builder.Property(x => x.Ordinal).HasColumnName("ordinal").HasComment("序号");
        //可编辑
        builder.Property(x => x.Editable).HasColumnName("editable").HasComment("可编辑");
        //状态
        builder.Property(x => x.Status).HasColumnName("status").HasComment("状态");
        //一对多设定
        builder.HasOne(x => x.Dict).WithMany(y => y.DictItems).HasForeignKey(f => f.DictId).OnDelete(DeleteBehavior.Restrict);
        //ToTable
        builder.ToTable("dict_item");
    }
}