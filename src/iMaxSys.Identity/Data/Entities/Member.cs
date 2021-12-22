//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: Member.cs
//摘要: 成员 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;
using iMaxSys.Data.Entities;

namespace iMaxSys.Identity.Data.Entities;

/// <summary>
/// Member
/// </summary>
public class Member : TenantMasterEntity
{
    /// <summary>
    /// 接入系统的用户Id
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 速查码
    /// </summary>
    public string QuickCode { get; set; } = string.Empty;

    /// <summary>
    /// 性别(0男,1女,2未知)
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    public DateTime Birthday { get; set; }

    /// <summary>
    /// 登录名/用户名
    /// </summary>
    public string LoginName { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 盐
    /// </summary>
    public string Salt { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 手机号码
    /// </summary>
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// 国家
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// 省
    /// </summary>
    public string Province { get; set; } = string.Empty;

    /// <summary>
    /// 城市
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    public int Type { get; set; } = 0;

    /// <summary>
    /// 访问失败次数
    /// </summary>
    public int FailedCount { get; set; } = 0;

    /// <summary>
    /// DepartmentId
    /// </summary>
    public long DepartmentId { get; set; }

    /// <summary>
    /// 应用来源
    /// </summary>
    public XppSource XppSource { get; set; }

    /// <summary>
    /// 账号来源
    /// </summary>
    public PlatformSource AccountSource { get; set; }

    /// <summary>
    /// 启用时间
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// 停用时间
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// 加入/激活时间
    /// </summary>
    public DateTime JoinTime { get; set; }

    /// <summary>
    /// 加入Ip
    /// </summary>
    public string JoinIp { get; set; } = string.Empty;

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime LastLogin { get; set; }

    /// <summary>
    /// 最后登录IP
    /// </summary>
    public string LastIp { get; set; } = string.Empty;

    /// <summary>
    /// 是否正式
    /// </summary>
    public bool IsOfficial { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    public virtual Department Department { get; set; } = new();

    /// <summary>
    /// 会员扩展
    /// </summary>
    public virtual IList<MemberExt>? MemberExts { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public virtual IList<RoleMember>? RoleMembers { get; set; }
}