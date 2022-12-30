//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
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

using iMaxSys.Max.Caching;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories;
using iMaxSys.Identity.Models;
using DbMember = iMaxSys.Identity.Data.Entities.Member;

namespace iMaxSys.Identity.Data.Repositories;

public interface IMemberRepository : IRepository<DbMember>
{
    /// <summary>
    /// find
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    Task<DbMember> FindAsync(long memberId);

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="memberId">成员id</param>
    /// <returns></returns>
    Task<IMember?> GetAsync(long memberId);

    /// <summary>
    /// RemoveAsync
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    Task RemoveAsync(long memberId);

    /// <summary>
    /// 移除访问session
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task RemoveAccessSessionAsync(string token);

    /// <summary>
    /// 获取访问Chain
    /// </summary>
    /// <param name="token">令牌</param>
    /// <returns></returns>
    Task<IAccessChain?> GetAccessChainAsync(string token);

    /// <summary>
    /// 获取访问Session
    /// </summary>
    /// <param name="token">令牌</param>
    /// <returns></returns>
    Task<IAccessSession?> GetAccessSessionAsync(string token);

    /// <summary>
    /// 刷新访问Chain
    /// </summary>
    /// <param name="accessChain"></param>
    /// <param name="oldToken">旧token</param>
    /// <returns></returns>
    Task RefreshAccessSessionAsync(IAccessChain accessChain, string? oldToken = null);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<IMember> RefreshMemberAsync(long memberId, IUser? user = null);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="member"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<IMember> RefreshMemberAsync(IMember member, IUser? user = null);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task RefreshUserAsync(IUser user);
}