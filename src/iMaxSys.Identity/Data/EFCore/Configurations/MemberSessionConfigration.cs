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

using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using iMaxSys.Max.Data.EFCore.Configurations;
using iMaxSys.Identity.Data.Models;

namespace iMaxSys.Identity.Data.EFCore.Configurations
{
    /// <summary>
    /// MemberSession映射配置
    /// </summary>
    public class MemberSessionConfiguration : TenantMasterEntityConfiguration<MemberSession>
    {
        protected override void Configures(EntityTypeBuilder<MemberSession> builder)
        {
            //基类配置
            base.Configures(builder);
            //应用x社交账号Id
            builder.Property(x => x.XppSnsId).HasColumnName("xpp_sns_id").IsRequired();
            //会员Id
            builder.Property(x => x.MemberId).HasColumnName("member_id").IsRequired();
            //TokenForMaxOne
            builder.Property(x => x.Token).HasColumnName("token").HasMaxLength(50).IsRequired();
            //第三方平台统一Id
            builder.Property(x => x.UnionId).HasColumnName("union_id").HasMaxLength(50).IsRequired();
            //第三方平台Id
            builder.Property(x => x.OpenId).HasColumnName("open_id").HasMaxLength(50).IsRequired();
            //SessionKey
            builder.Property(x => x.SessionKey).HasColumnName("session_key").HasMaxLength(50);
            //名字
            builder.Property(x => x.NickName).HasColumnName("nick_name").HasMaxLength(50).IsRequired();
            //头像
            builder.Property(x => x.Avatar).HasColumnName("avatar").HasMaxLength(500).IsRequired();
            //Token过期时间
            builder.Property(x => x.Expires).HasColumnName("expires").IsRequired();
            //是否正式成员
            builder.Property(x => x.IsOfficial).HasColumnName("is_official").IsRequired();
            //IP
            builder.Property(x => x.Ip).HasColumnName("ip").HasMaxLength(50).IsRequired();
            //状态
            builder.Property(x => x.Status).HasColumnName("status").IsRequired();
            //XappSns
            //builder.HasOne(x => x.XappSns).WithMany(x => x.Sessions).HasForeignKey(f => f.XappSnsId);
            //索引
            builder.HasIndex(x => new { x.XppSnsId, x.Token });
            //ToTable
            builder.ToTable("member_session");
        }
    }
}


