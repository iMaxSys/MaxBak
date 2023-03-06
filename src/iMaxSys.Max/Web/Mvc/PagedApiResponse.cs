//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: 分页响应范型类.cs
//摘要: PagingResponse
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-12
//----------------------------------------------------------------

using iMaxSys.Max.Common;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Collection;

namespace iMaxSys.Max.Web.Mvc;

/// <summary>
/// 分页响应范型类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PagedApiResponse<T> : PagedList<T>, IApiResponse
{
}