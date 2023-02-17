//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMember.cs
//摘要: IMember
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// IMember
/// 唯一标识: <see cref="Id"/>
/// </summary>
public interface IMember
{
    /// <summary>
    /// Id
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// TenantId
    /// </summary>
    long TenantId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// 性别(0男,1女,2未知)
    /// </summary>
    Gender? Gender { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    DateTime? Birthday { get; set; }

    /// <summary>
    /// 婚姻状况
    /// </summary>
    public MaritalStatus? MaritalStatus { get; set; }

    /// <summary>
    /// 民族(默认0:未知)
    /// </summary>
    public int? Nation { get; set; }

    /// <summary>
    /// 教育程度(默认0:未知)
    /// </summary>
    public int? Education { get; set; }

    /// <summary>
    /// 政党(默认0:未知)
    /// </summary>
    public int? Party { get; set; }

    /// <summary>
    /// 身份证号
    /// </summary>
    string? IdNumber { get; set; }

    /// <summary>
    /// 速查码
    /// </summary>
    string? QuickCode { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    string? UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    string? NickName { get; set; }

    /// <summary>
    /// 国家代码(默认中国:86)
    /// </summary>
    int? CountryCode { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    long Mobile { get; set; }

    /// <summary>
    /// 电话号码
    /// </summary>
    string? Phone { get; set; }

    /// <summary>
    /// 电子邮箱
    /// </summary>
    string? Email { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    string? Avatar { get; set; }

    /// <summary>
    /// 国家
    /// </summary>
    string? Country { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    string? Province { get; set; }

    /// <summary>
    /// 城市
    /// </summary>
    string? City { get; set; }

    /// <summary>
    /// 区/县
    /// </summary>
    string? District { get; set; }

    /// <summary>
    /// 街道/乡镇
    /// </summary>
    string? Street { get; set; }

    /// <summary>
    /// 社区/村
    /// </summary>
    string? Community { get; set; }

    /// <summary>
    /// 区域代码from国家统计局
    /// </summary>
    long? AreaCode { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    string? Address { get; set; }

    /// <summary>
    /// 邮编
    /// </summary>
    string? Zipcode { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    int Type { get; set; }

    /// <summary>
    /// 访问失败次数
    /// </summary>
    int FailedCount { get; set; }

    /// <summary>
    /// DepartmentId
    /// </summary>
    long DepartmentId { get; set; }

    /// <summary>
    /// 应用来源
    /// </summary>
    XppSource XppSource { get; set; }

    /// <summary>
    /// 账号来源
    /// </summary>
    SnsSource AccountSource { get; set; }

    /// <summary>
    /// 启用时间
    /// </summary>
    DateTime? Start { get; set; }

    /// <summary>
    /// 停用时间
    /// </summary>
    DateTime? End { get; set; }

    /// <summary>
    /// 加入/激活时间
    /// </summary>
    DateTime JoinTime { get; set; }

    /// <summary>
    /// 加入Ip
    /// </summary>
    string? JoinIp { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    DateTime? LastLogin { get; set; }

    /// <summary>
    /// 最后登录IP
    /// </summary>
    string? LastIp { get; set; }

    /// <summary>
    /// 是否正式
    /// </summary>
    bool IsOfficial { get; set; }

    /// <summary>
    /// 用户Id
    /// </summary>
    long UserId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    Status Status { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    List<Role>? Roles { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    Department? Department { get; set; }

    /// <summary>
    /// 租户
    /// </summary>
    Tenant Tenant { get; set; }
}

/// <summary>
/// Member<T>
/// </summary>
public interface IMember<T> : IMember
{
    /// <summary>
    /// 具体用户
    /// </summary>
    T? Info { get; set; }
}