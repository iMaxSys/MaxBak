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
    /// Member映射配置
    /// </summary>
    public class MemberConfiguration : TenantMasterEntityConfiguration<Member>
    {
        protected override void Configures(EntityTypeBuilder<Member> builder)
        {
            //基类配置
            base.Configures(builder);
            //扩展Id
            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            //姓名
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            //速查码
            builder.Property(x => x.QuickCode).HasColumnName("quick_code").HasMaxLength(50).IsRequired();
            //性别(0男,1女,2未知)
            builder.Property(x => x.Gender).HasColumnName("gender").IsRequired();
            //生日
            builder.Property(x => x.Birthday).HasColumnName("birthday").IsRequired();
            //登录名
            builder.Property(x => x.LoginName).HasColumnName("login_name").HasMaxLength(50).IsRequired();
            //密码
            builder.Property(x => x.Password).HasColumnName("password").HasMaxLength(50).IsRequired();
            //盐
            builder.Property(x => x.Salt).HasColumnName("salt").HasMaxLength(50).IsRequired();
            //昵称
            builder.Property(x => x.NickName).HasColumnName("nick_name").HasMaxLength(50).IsRequired();
            //手机号码
            builder.Property(x => x.Mobile).HasColumnName("mobile").HasMaxLength(50).IsRequired();
            //电子邮箱
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(50).IsRequired();
            //头像
            builder.Property(x => x.Avatar).HasColumnName("avatar").HasMaxLength(500).IsRequired();
            //类型
            builder.Property(x => x.Type).HasColumnName("type").IsRequired();
            //登录失败次数
            builder.Property(x => x.FailedCount).HasColumnName("failed_count").IsRequired();
            //RoleId
            builder.Property(x => x.RoleId).HasColumnName("role_id").IsRequired();
            //DepartmentId
            builder.Property(x => x.DepartmentId).HasColumnName("department_id").IsRequired();
            //应用来源
            builder.Property(x => x.XppSource).HasColumnName("xpp_source").IsRequired();
            //账号来源
            builder.Property(x => x.AccountSource).HasColumnName("account_source").IsRequired();
            //启用时间
            builder.Property(x => x.Start).HasColumnName("start").IsRequired();
            //停用时间
            builder.Property(x => x.End).HasColumnName("end").IsRequired().IsRequired();
            //加入/激活时间
            builder.Property(x => x.JoinTime).HasColumnName("join_time").IsRequired();
            //加入Ip
            builder.Property(x => x.JoinIp).HasColumnName("join_ip").HasMaxLength(50).IsRequired();
            //最后登录时间
            builder.Property(x => x.LastLogin).HasColumnName("last_login").IsRequired();
            //最后登录IP
            builder.Property(x => x.LastIp).HasColumnName("last_ip").HasMaxLength(50).IsRequired();
            //是否正式会员
            builder.Property(x => x.IsOfficial).HasColumnName("is_official").HasDefaultValue(false);
            //状态
            builder.Property(x => x.Status).HasColumnName("status").IsRequired();
            //部门关系
            builder.HasOne(x => x.Department).WithMany(x => x.Members).HasForeignKey(f => f.DepartmentId);
            //角色关系
            builder.HasOne(x => x.Role).WithMany(x => x.Members).HasForeignKey(f => f.RoleId);
            //索引
            builder.HasIndex(x => new { x.Name });
            builder.HasIndex(x => new { x.LoginName });
            builder.HasIndex(x => new { x.Mobile });
            //ToTable
            builder.ToTable("member");
        }
    }
}


