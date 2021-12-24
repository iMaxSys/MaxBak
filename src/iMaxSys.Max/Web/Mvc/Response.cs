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

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Web.Mvc;

public interface IResponseBase
{
}

/// <summary>
/// 响应类
/// </summary>
public class Response : Result, IResponseBase
{
}

/// <summary>
/// 响应范型类
/// </summary>
public class Response<T> : Result<T>, IResponseBase
{
}