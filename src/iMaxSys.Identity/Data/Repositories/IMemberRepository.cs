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

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories;
using DbMember = iMaxSys.Identity.Data.Entities.Member;
using iMaxSys.Identity.Models;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Exceptions;

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
    /// 获取访问Session
    /// </summary>
    /// <param name="token">令牌</param>
    /// <returns></returns>
    Task<IAccessSession?> GetAccessSessionAsync(string token);

    /// <summary>
    /// 刷新访问session
    /// </summary>
    /// <param name="oldToken"></param>
    /// <param name="accessSession"></param>
    /// <param name="member"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    Task RefreshAccessSessionAsync(string oldToken, IAccessSession accessSession, IMember? member = null, IUser? user = null);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    Task RefreshMemberAsync(long memberId, IUser? user = null);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="member"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    Task RefreshMemberAsync(IMember member, IUser? user = null);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task RefreshUserAsync(IUser user);
}