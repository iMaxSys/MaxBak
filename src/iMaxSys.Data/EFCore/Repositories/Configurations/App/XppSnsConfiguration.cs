//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: XappSnsConfiguration.cs
//摘要: 应用x社交网络映射配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Data.Entities.App;

namespace iMaxSys.Data.EFCore.Repositories.Configurations.App;

/// <summary>
/// 应用x社交网络映射配置
/// </summary>
public class XppSnsConfiguration : MasterEntityConfiguration<XppSns>
{
    protected override void Configures(EntityTypeBuilder<XppSns> builder)
    {
        //基类配置
        base.Configures(builder);
        //名称
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        //Alias
        builder.Property(x => x.Alias).HasColumnName("alias").HasMaxLength(50).IsRequired();
        //Description
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(255);
        //应用Id
        builder.Property(x => x.XppId).HasColumnName("xpp_id").IsRequired();
        //社交平台账号来源
        builder.Property(x => x.Source).HasColumnName("source").IsRequired();
        //第三方平台原始Id
        builder.Property(x => x.AccountId).HasColumnName("account_id").HasMaxLength(50).IsRequired();
        //AppId
        builder.Property(x => x.AppId).HasColumnName("app_id").HasMaxLength(50);
        //AppSecret
        builder.Property(x => x.AppSecret).HasColumnName("app_secret").HasMaxLength(50);
        //状态
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //index
        builder.HasIndex(x => new { x.Name });
        //一对多设定
        builder.HasOne(x => x.Xpp).WithMany(y => y.XppSns).HasForeignKey(f => f.XppId).OnDelete(DeleteBehavior.Restrict);
        //ToTable
        builder.ToTable("xpp_sns");
    }
}