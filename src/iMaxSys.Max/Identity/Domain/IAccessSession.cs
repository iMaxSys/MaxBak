﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IAccessSession.cs
//摘要: 访问令牌Session
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// 访问令牌Session
/// </summary>
public interface IAccessSession
{
    /// <summary>
    /// XppId
    /// </summary>
    long XppId { get; set; }

    /// <summary>
    /// XppSnsId
    /// </summary>
    long XppSnsId { get; set; }

    /// <summary>
    /// 来源Id/应用来源
    /// </summary>
    XppSource XppSource { get; set; }

    /// <summary>
    /// 账号来源/社交来源
    /// </summary>
    SnsSource AccountSource { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    long? TenantId { get; set; }

    /// <summary>
    /// 成员id
    /// </summary>
    long MemberId { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    long UserId { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    int Type { get; set; }

    /// <summary>
    /// Token
    /// </summary>
    string Token { get; set; }

    /// <summary>
    /// Token过期时间
    /// </summary>
    DateTime Expires { get; set; }

    /// <summary>
    /// 第三方平台原始Id
    /// </summary>
    string? AccountId { get; set; }

    /// <summary>
    /// 第三方平台App
    /// </summary>
    string? AppId { get; set; }

    /// <summary>
    /// 第三方平台Id
    /// </summary>
    string? OpenId { get; set; }

    /// <summary>
    /// SessionKey
    /// </summary>
    string? SessionKey { get; set; }

    /// <summary>
    /// 名字
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    string? Avatar { get; set; }

    /// <summary>
    /// 第三方平台统一Id
    /// </summary>
    string? UnionId { get; set; }

    /// <summary>
    /// 是否正式用户
    /// </summary>
    bool IsOfficial { get; set; }

    /// <summary>
    /// 是否登录
    /// </summary>
    bool IsLogin { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    Status Status { get; set; }
}
