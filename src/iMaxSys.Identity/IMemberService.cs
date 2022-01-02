//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMemberService.cs
//摘要: 菜单服务接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity.Models;

namespace iMaxSys.Identity;

public interface IMemberService : IDependency
{
    /// <summary>
    /// 获取AccessChain
    /// </summary>
    /// <param name="token">令牌</param>
    /// <returns></returns>
    Task<IAccessChain?> GetAccessChainAsync(string token);

    /// <summary>
    /// Add member
    /// </summary>
    /// <param name="model"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<IMember> AddAsync(MemberModel model, long xppId, long roleId);

    /// <summary>
    /// 移除成员
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    Task RemoveAsync(long memberId);

    /// <summary>
    /// Get member
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IMember?> GetAsync(long id);

    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task UpdateAsync(MemberModel model);

    /// <summary>
    /// 刷新成员缓存
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    //Task RefreshAsync(long memberId);

    /// <summary>
    /// 刷新成员缓存
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    Task RefreshAsync(IMember member);

    /// <summary>
    /// 刷新AcceeChain缓存
    /// </summary>
    /// <param name="oldToken"></param>
    /// <param name="memberId"></param>
    /// <returns></returns>
    //Task RefreshAcceeChainAsync(string oldToken, long memberId);
}