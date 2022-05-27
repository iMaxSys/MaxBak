//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ICacheFactory.cs
//摘要: ICacheFactory
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Caching;

public interface ICacheFactory
{
    /// <summary>
    /// 获取缓存服务
    /// </summary>
    /// <param name="source"></param>
    /// <param name="connection"></param>
    /// <returns></returns>
    ICache GetService(int source, string connection);

    /// <summary>
    /// 获取缓存服务from config
    /// </summary>
    /// <returns></returns>
    ICache GetService();
}
