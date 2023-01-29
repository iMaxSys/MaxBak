//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Member.cs
//摘要: Member
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// Member
/// </summary>
public class Member : IMember
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// TenantId
    /// </summary>
    public long TenantId { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 性别(0男,1女,2未知)
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    public DateTime? Birthday { get; set; }

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
    public string? IdNumber { get; set; }

    /// <summary>
    /// 速查码
    /// </summary>
    public string? QuickCode { get; set; }

    /// <summary>
    /// 登录名/用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 国家代码(默认中国:86)
    /// </summary>
    public int? CountryCode { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    public long Mobile { get; set; }

    /// <summary>
    /// 电话号码
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 国家
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 城市
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 区/县
    /// </summary>
    public string? District { get; set; }

    /// <summary>
    /// 街道/乡镇
    /// </summary>
    public string? Street { get; set; }

    /// <summary>
    /// 社区/村
    /// </summary>
    public string? Community { get; set; }

    /// <summary>
    /// 区域代码from国家统计局
    /// </summary>
    public long? AreaCode { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 邮编
    /// </summary>
    public string? Zipcode { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 访问失败次数
    /// </summary>
    public int FailedCount { get; set; }

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
    public SnsSource AccountSource { get; set; }

    /// <summary>
    /// 启用时间
    /// </summary>
    public DateTime? Start { get; set; }

    /// <summary>
    /// 停用时间
    /// </summary>
    public DateTime? End { get; set; }

    /// <summary>
    /// 加入/激活时间
    /// </summary>
    public DateTime JoinTime { get; set; }

    /// <summary>
    /// 加入Ip
    /// </summary>
    public string? JoinIp { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    /// 最后登录IP
    /// </summary>
    public string? LastIp { get; set; }

    /// <summary>
    /// 是否正式
    /// </summary>
    public bool IsOfficial { get; set; }

    /// <summary>
    /// UserId
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public IList<IRole>? Roles { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    public IDepartment? Department { get; set; }

    /// <summary>
    /// 租户
    /// </summary>
    public ITenant Tenant { get; set; } = new Tenant();
}

public class Member<T> : Member, IMember<T>
{
    public T? Info { get; set; }
}