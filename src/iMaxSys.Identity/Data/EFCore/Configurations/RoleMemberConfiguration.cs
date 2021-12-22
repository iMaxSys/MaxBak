//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: RoleMemberConfiguration.cs
//摘要: 会员配置
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
/// RoleMember映射配置
/// </summary>
public class RoleMemberConfiguration : TenantMasterEntityConfiguration<RoleMember>
{
    protected override void Configures(EntityTypeBuilder<RoleMember> builder)
    {
        //基类配置
        base.Configures(builder);
        //MemberId
        builder.Property(x => x.MemberId).HasColumnName("member_id").IsRequired();
        //RoleId
        builder.Property(x => x.RoleId).HasColumnName("role_id").IsRequired();
        //Status
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        //关系
        builder.HasOne(x => x.Role).WithMany(x => x.RoleMembers).HasForeignKey(x => x.RoleId);
        //关系
        builder.HasOne(x => x.Member).WithMany(x => x.RoleMembers).HasForeignKey(x => x.MemberId);
        //Index
        builder.HasIndex(x => new { x.TenantId, x.MemberId });
        builder.HasIndex(x => new { x.TenantId, x.RoleId });
        //ToTable
        builder.ToTable("role_member");
    }
}