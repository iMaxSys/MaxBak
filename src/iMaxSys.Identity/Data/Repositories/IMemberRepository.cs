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

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories;
using iMaxSys.Identity.Data.Entities;
using DbMember = iMaxSys.Identity.Data.Entities.Member;

namespace iMaxSys.Identity.Data.Repositories;

public interface IMemberRepository : IRepository<DbMember>
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
    Task<IMember?> ReadAsync(long memberId);

    /// <summary>
    /// RemoveAsync
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    Task RemoveAsync(long memberId);

    /// <summary>
    /// 刷新member缓存
    /// </summary>
    /// <param name="member"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    Task RefreshAsync(IMember member, DateTime expires);

    /// <summary>
    /// 刷新AccessChain
    /// </summary>
    /// <param name="oldToken"></param>
    /// <param name="accessChain"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    Task RefreshAccessChainAsync(string oldToken, IAccessChain accessChain, DateTime expires);
}