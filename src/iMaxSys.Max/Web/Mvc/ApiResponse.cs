//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: 响应类.cs
//摘要: Response
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-12
//----------------------------------------------------------------

using iMaxSys.Max.Common;

namespace iMaxSys.Max.Web.Mvc;

/// <summary>
/// 响应接口
/// </summary>
public interface IApiResponse
{
}

/// <summary>
/// 响应类
/// </summary>
public abstract class ApiResponse : IApiResponse
{
}

/// <summary>
/// 响应范型接口
/// </summary>
public interface IApiResponse<T> : IApiResponse
{
    /// <summary>
    /// 数据
    /// </summary>
    public T? Data { get; set; }
}

/// <summary>
/// 响应范型类
/// </summary>
public abstract class ApiResponse<T> : ApiResponse, IApiResponse<T>
{
    /// <summary>
    /// 数据
    /// </summary>
    public T? Data { get; set; }
}