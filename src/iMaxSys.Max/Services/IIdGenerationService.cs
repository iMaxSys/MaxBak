//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IIdGenerationService.cs
//摘要: IIdGenerationService
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Services;
/// <summary>
/// IIdGenerationService(Singleton)
/// </summary>
public interface IIdGenerationService : ISingleton

{
    /// <summary>
    /// 获取唯一Id
    /// </summary>
    /// <returns></returns>
    long NextId();
}
