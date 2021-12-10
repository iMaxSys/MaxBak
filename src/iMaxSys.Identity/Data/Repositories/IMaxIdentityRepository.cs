//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMaxIdentityRepository.cs
//摘要: 身份通用仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Data;
using iMaxSys.Max.Data.Entities;
using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Identity.Data.Repositories
{
    /// <summary>
    /// 身份通用仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMaxIdentityRepository<T> : IRepository<T> where T : Entity
    {
    }
}
