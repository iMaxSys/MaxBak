//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: AccessConfig.cs
//摘要: 访问配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-02-02
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;
using iMaxSys.Sns.Common;

namespace iMaxSys.Sns.Api;

/// <summary>
/// 访问配置
/// </summary>
public class AccessConfig
{
    /// <summary>
    /// 租户Id
    /// </summary>
    public long TenantId { get; set; }
    
    /// <summary>
    /// 账号来源
    /// </summary>
    public SnsSource SnsSource { get; set; }

    /// <summary>
    /// AccountId（第三方平台主体Id,例如微信公众号原始Id）
    /// </summary>
    public string AccountId { get; set; } = String.Empty;

    /// <summary>
    /// AppId
    /// </summary>
    public string AppId { get; set; } = String.Empty;

    /// <summary>
    /// AppSecret
    /// </summary>
    public string AppSecret { get; set; } = String.Empty;

    /// <summary>
    /// SessionKey
    /// </summary>
    public string SessionKey { get; set; } = String.Empty;

    /// <summary>
    /// OpenId
    /// </summary>
    public string OpenId { get; set; } = String.Empty;

    /// <summary>
    /// Avatar
    /// </summary>
    public string Avatar { get; set; } = String.Empty;

    /// <summary>
    /// Name
    /// </summary>
    public string NickName { get; set; } = String.Empty;

    /// <summary>
    /// UnionId
    /// </summary>
    public string UnionId { get; set; } = String.Empty;

    /// <summary>
    /// 令牌
    /// </summary>
    public string Token { get; set; } = String.Empty;

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime Expires { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;
}