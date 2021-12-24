//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MemberModel.cs
//摘要: 成员模型
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 成员模型
/// </summary>
public class MemberModel
{
    /// <summary>
    /// Id
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// ExtId
    /// </summary>
    public long? ExtId { get; set; }

    /// <summary>
    /// TenantId
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// 速查码
    /// </summary>
    public string QuickCode { get; set; } = String.Empty;

    /// <summary>
    /// 性别(0男,1女,2未知)
    /// </summary>
    public Gender Gender { get; set; } = Gender.Unknown;

    /// <summary>
    /// 生日
    /// </summary>
    public DateTime Birthday { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = String.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = String.Empty;

    /// <summary>
    /// 手机号码
    /// </summary>
    public string Mobile { get; set; } = String.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string Email { get; set; } = String.Empty;

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; } = String.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    public int? Type { get; set; }

    /// <summary>
    /// RoleId
    /// </summary>
    public long? RoleId { get; set; }

    /// <summary>
    /// DepartmentId
    /// </summary>
    public long? DepartmentId { get; set; }

    /// <summary>
    /// 启用时间
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// 停用时间
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// 是否正式
    /// </summary>
    public bool IsOfficial { get; set; } = false;

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;
}