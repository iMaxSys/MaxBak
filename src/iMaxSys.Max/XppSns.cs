﻿//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: BizConfig.cs
//摘要: 业务配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max;

public class XppSns
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 应用Id
    /// </summary>
    public long XppId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 别名
    /// </summary>
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 社交平台账号来源
    /// </summary>
    public SnsSource Source { get; set; }

    /// <summary>
    /// 第三方平台原始Id
    /// </summary>
    public string AccountId { get; set; } = string.Empty;

    /// <summary>
    /// AppId
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// AppKey
    /// </summary>
    public string AppKey { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Xpp
    /// </summary>
    public Xpp Xpp { get; set; } = new();
}

