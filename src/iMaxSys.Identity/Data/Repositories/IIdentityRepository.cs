//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IIDentityRepository.cs
//摘要: 身份仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;

namespace iMaxSys.Identity.Data.Repositories;

public interface IIdentityRepository<T> : IRepository<T> where T : Entity
{
}

public interface IIdentityReadOnlyRepository<T> : IReadOnlyRepository<T> where T : Entity
{
}

/*
/// <summary>
/// 身份仓储
/// </summary>
public interface IIdentityRepository : IRepositoryBase
{
    /// <summary>
    /// 获取访问Session
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IAccessSession?> GetAccessSessionAsync(string token);

    /// <summary>
    /// 读取成员
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    Task<IMember?> ReadAsync(string memberId);
}
*/