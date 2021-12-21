//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ISerialService.cs
//摘要: ISerialService
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Services;

/// <summary>
/// 序列号服务
/// </summary>
public interface ISerialService : ISingleton

{
    /// <summary>
    /// 获取下一流水号
    /// </summary>
    /// <param name="id">业务Id</param>
    /// <returns></returns>
    Task<int> Next(string id);
}