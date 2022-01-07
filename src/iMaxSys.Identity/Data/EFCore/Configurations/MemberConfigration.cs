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
using iMaxSys.Data.Repositories.EFCore.Configurations;

namespace iMaxSys.Identity.Data.EFCore.Configurations;

/// <summary>
/// Member映射配置
/// </summary>
public class MemberConfiguration : TenantMasterEntityConfiguration<Member>
{
    protected override void Configures(EntityTypeBuilder<Member> builder)
    {
        //基类配置
        base.Configures(builder);
        //外接Id
        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired().HasComment("外接Id");
        //姓名
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired().HasComment("姓名");
        //身份证号码
        builder.Property(x => x.IdNumber).HasColumnName("id_number").HasMaxLength(50).IsRequired().HasComment("身份证号码");
        //速查码
        builder.Property(x => x.QuickCode).HasColumnName("quick_code").HasMaxLength(50).IsRequired().HasComment("速查码");
        //生日
        builder.Property(x => x.Birthday).HasColumnName("birthday").IsRequired().HasComment("生日");
        //婚姻状况
        builder.Property(x => x.MaritalStatus).HasColumnName("marital_status").IsRequired().HasComment("婚姻状况");
        //性别(0男,1女,2未知)
        builder.Property(x => x.Gender).HasColumnName("gender").IsRequired().HasComment("性别(0男,1女,2未知)");
        //民族(默认0:未知)
        builder.Property(x => x.Nation).HasColumnName("nation").IsRequired().HasComment("民族(默认0:未知)");
        //教育程度(默认0:未知)
        builder.Property(x => x.Education).HasColumnName("education").IsRequired().HasComment("教育程度(默认0:未知)");
        //政党(默认0:未知)
        builder.Property(x => x.Party).HasColumnName("party").IsRequired().HasComment("政党(默认0:未知)");
        //登录名
        builder.Property(x => x.UserName).HasColumnName("user_name").HasMaxLength(50).IsRequired().HasComment("登录名");
        //密码
        builder.Property(x => x.Password).HasColumnName("password").HasMaxLength(50).IsRequired().HasComment("密码");
        //盐
        builder.Property(x => x.Salt).HasColumnName("salt").HasMaxLength(50).IsRequired().HasComment("盐");
        //昵称
        builder.Property(x => x.NickName).HasColumnName("nick_name").HasMaxLength(50).IsRequired().HasComment("昵称");
        //国家代码(默认中国:86)
        builder.Property(x => x.CountryCode).HasColumnName("country_code").IsRequired().HasComment("国家代码(默认中国:86)");
        //移动电话号码
        builder.Property(x => x.Mobile).HasColumnName("mobile").HasMaxLength(50).IsRequired().HasComment("移动电话号码");
        //电话号码
        builder.Property(x => x.Phone).HasColumnName("phone").HasMaxLength(50).IsRequired().HasComment("电话号码");
        //电子邮箱
        builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(50).IsRequired().HasComment("电子邮箱");
        //头像
        builder.Property(x => x.Avatar).HasColumnName("avatar").HasMaxLength(255).IsRequired().HasComment("头像");
        //国家
        builder.Property(x => x.Country).HasColumnName("country").HasMaxLength(50).IsRequired().HasComment("国家");
        //省
        builder.Property(x => x.Province).HasColumnName("province").HasMaxLength(50).IsRequired().HasComment("省");
        //市
        builder.Property(x => x.City).HasColumnName("city").HasMaxLength(50).IsRequired().HasComment("市");
        //区/县
        builder.Property(x => x.District).HasColumnName("district").HasMaxLength(50).IsRequired().HasComment("区/县");
        //街道/乡镇
        builder.Property(x => x.Street).HasColumnName("street").HasMaxLength(50).IsRequired().HasComment("街道/乡镇");
        //社区/村
        builder.Property(x => x.Community).HasColumnName("community").HasMaxLength(50).IsRequired().HasComment("社区/村");
        //区域代码from国家统计局
        builder.Property(x => x.AreaCode).HasColumnName("area_code").IsRequired().HasComment("区域代码from国家统计局");
        //地址
        builder.Property(x => x.Address).HasColumnName("address").HasMaxLength(255).IsRequired().HasComment("地址");
        //邮编
        builder.Property(x => x.Zipcode).HasColumnName("zipcode").HasMaxLength(50).IsRequired().HasComment("邮编");
        //类型
        builder.Property(x => x.Type).HasColumnName("type").IsRequired().HasComment("类型");
        //登录失败次数
        builder.Property(x => x.FailedCount).HasColumnName("failed_count").IsRequired().HasComment("登录失败次数");
        //部门id
        builder.Property(x => x.DepartmentId).HasColumnName("department_id").IsRequired().HasComment("部门id");
        //应用来源
        builder.Property(x => x.XppSource).HasColumnName("xpp_source").IsRequired().HasComment("应用来源");
        //账号来源
        builder.Property(x => x.AccountSource).HasColumnName("account_source").IsRequired().HasComment("账号来源");
        //启用时间
        builder.Property(x => x.Start).HasColumnName("start").IsRequired().HasComment("启用时间");
        //停用时间
        builder.Property(x => x.End).HasColumnName("end").IsRequired().IsRequired().HasComment("停用时间");
        //加入/激活时间
        builder.Property(x => x.JoinTime).HasColumnName("join_time").IsRequired().HasComment("加入/激活时间");
        //加入Ip
        builder.Property(x => x.JoinIP).HasColumnName("join_ip").HasMaxLength(50).IsRequired().HasComment("加入Ip");
        //最后登录时间
        builder.Property(x => x.LastLogin).HasColumnName("last_login").IsRequired().HasComment("最后登录时间");
        //最后登录IP
        builder.Property(x => x.LastIP).HasColumnName("last_ip").HasMaxLength(50).IsRequired().HasComment("最后登录IP");
        //是否正式会员
        builder.Property(x => x.IsOfficial).HasColumnName("is_official").HasComment("是否正式成员");
        //状态
        builder.Property(x => x.Status).HasColumnName("status").IsRequired().HasComment("状态");
        //部门关系
        builder.HasOne(x => x.Department).WithMany(x => x.Members).HasForeignKey(f => f.DepartmentId);
        //索引
        builder.HasIndex(x => new { x.IdNumber });
        builder.HasIndex(x => new { x.Name });
        builder.HasIndex(x => new { x.UserName });
        builder.HasIndex(x => new { x.Mobile });
        //ToTable
        builder.ToTable("member").HasComment("成员");
    }
}