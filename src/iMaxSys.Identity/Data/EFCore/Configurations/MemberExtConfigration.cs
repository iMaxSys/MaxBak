//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: MemberExtConfiguration.cs
//摘要: 会员账号扩展配置
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
/// 会员扩展映射配置
/// </summary>
public class MemberExtConfiguration : TenantMasterEntityConfiguration<MemberExt>
{
    protected override void Configures(EntityTypeBuilder<MemberExt> builder)
    {
        //基类配置
        base.Configures(builder);
        //应用x社交账号Id
        builder.Property(x => x.XppSnsId).HasColumnName("xpp_sns_id").IsRequired();
        //会员Id
        builder.Property(x => x.MemberId).HasColumnName("member_id").IsRequired();
        //OpenId
        builder.Property(x => x.OpenId).HasColumnName("open_id").HasMaxLength(50).IsRequired();
        //Token
        builder.Property(x => x.Token).HasColumnName("token").HasMaxLength(50);
        //名称
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        //昵称
        builder.Property(x => x.NickName).HasColumnName("nick_name").HasMaxLength(50).IsRequired();
        //PhoneNumber
        builder.Property(x => x.Mobile).HasColumnName("mobile").HasMaxLength(50).IsRequired();
        //国家
        builder.Property(x => x.Country).HasColumnName("country").HasMaxLength(50);
        //省
        builder.Property(x => x.Province).HasColumnName("province").HasMaxLength(50);
        //城市
        builder.Property(x => x.City).HasColumnName("city").HasMaxLength(50);
        //头像
        builder.Property(x => x.Avatar).HasColumnName("avatar").HasMaxLength(500).IsRequired();
        //性别
        builder.Property(x => x.Gender).HasColumnName("gender").IsRequired();
        //过期时间
        builder.Property(x => x.Expires).HasColumnName("expires").IsRequired();
        //状态
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //会员信息
        builder.HasOne(x => x.Member).WithMany(x => x.MemberExts).HasForeignKey(f => f.MemberId);
        //AppSns
        //builder.HasOne(x => x.XappSns).WithMany(x => x.Exts).HasForeignKey(f => f.XappSnsId);
        //索引
        builder.HasIndex(x => new { x.XppSnsId, x.OpenId }).IsUnique();
        //ToTable
        builder.ToTable("member_ext");
    }
}