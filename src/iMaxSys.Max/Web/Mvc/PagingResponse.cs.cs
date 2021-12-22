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

using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Collection;
using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Web.Mvc;

/// <summary>
/// 分页响应范型类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PagingResponse<T> : Response
{
    private PagedList<T>? _list;

    /// <summary>
    /// 数据
    /// </summary>
    public override object? Data
    {
        get
        {
            return _list;
        }
        set
        {
            _list = value is PagedList<T> ? value as PagedList<T> : throw new MaxException(ResultEnum.TypeIsUnmatched);
        }
    }
}