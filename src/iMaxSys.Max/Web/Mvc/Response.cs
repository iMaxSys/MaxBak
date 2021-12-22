//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: 响应抽象类.cs
//摘要: Response
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-12
//----------------------------------------------------------------

namespace iMaxSys.Max.Web.Mvc;

/// <summary>
/// 响应抽象类
/// </summary>
public abstract class Response
{
    /// <summary>
    /// 成功标志
    /// </summary>
    public bool Success { get; set; } = false;
    /// <summary>
    /// 代码
    /// </summary>
    public int Code { get; set; } = -1;
    /// <summary>
    /// 信息
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// 详情
    /// </summary>
    public string? Detail { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public virtual object? Data { get; set; }
}