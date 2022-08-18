//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: XappSns.cs
//摘要: 应用x社交账号 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Data.Entities.App;

/// <summary>
/// 应用x社交网络
/// </summary>
public class XppSns : MasterEntity
{
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
    /// 应用Id
    /// </summary>
    public long XppId { get; set; }

    /// <summary>
    /// 社交平台账号来源
    /// </summary>
    public SnsSource Source { get; set; }

    /// <summary>
    /// 第三方平台原始Id
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
    /// 状态
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// App
    /// </summary>
    public virtual Xpp Xpp { get; set; } = new();
}