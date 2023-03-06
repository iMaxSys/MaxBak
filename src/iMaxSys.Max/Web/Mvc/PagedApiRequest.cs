//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: 分页请求抽象类.cs
//摘要: PagingRequest
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-12
//----------------------------------------------------------------

namespace iMaxSys.Max.Web.Mvc;

/// <summary>
/// 分页请求
/// </summary>
public abstract class PagedApiRequest : ApiRequest
{
    const int SIZE = 25;

    int _index;
    int _size = SIZE;

    /// <summary>
    /// 索引(0+)
    /// </summary>
    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// 大小(1-25)
    /// </summary>
    public int Size
    {
        get
        {
            return _size;
        }
        set
        {
            _size = value < 1 ? 1 : (value > _size ? _size : value);
        }
    }
}