//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: CheckCodeConfiguration.cs
//摘要: CheckCode映射配置
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
/// CheckCode映射配置
/// </summary>
public class CheckCodeConfiguration : TenantMasterEntityConfiguration<CheckCode>
{
    protected override void Configures(EntityTypeBuilder<CheckCode> builder)
    {
        //基类配置
        base.Configures(builder);
        //应用Id
        builder.Property(x => x.XppId).HasColumnName("xpp_id").IsRequired();
        //业务Id
        builder.Property(x => x.BizId).HasColumnName("biz_id").IsRequired();
        //MemberId
        builder.Property(x => x.MemberId).HasColumnName("member_id").IsRequired();
        //目标
        builder.Property(x => x.To).HasColumnName("to").HasMaxLength(50).IsRequired();
        //验证码
        builder.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
        //内容
        builder.Property(x => x.Content).HasColumnName("content").HasMaxLength(255).IsRequired();
        //过期时间
        builder.Property(x => x.Expires).HasColumnName("expires").IsRequired();
        //校验次数
        builder.Property(x => x.CheckCount).HasColumnName("check_count").IsRequired();
        //状态
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //index
        builder.HasIndex(x => new { x.XppId, x.TenantId, x.BizId, x.MemberId, x.To, x.Expires, x.Status });
        //ToTable
        builder.ToTable("check_code");
    }
}