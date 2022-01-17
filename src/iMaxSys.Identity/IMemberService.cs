//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMemberService.cs
//摘要: 成员服务接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-01-04
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity.Models;
using iMaxSys.Sns.Common.Open;

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
    Task<IMember?> UpdateAsync(MemberModel model);

    /// <summary>
    /// 刷新成员缓存
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    Task RefreshAsync(long memberId);

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
    /// <param name="accessChain"></param>
    /// <returns></returns>
    Task RefreshAcceeChainAsync(string oldToken, IAccessChain accessChain);

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task RegisterAsync(RegisterModel model);

    /// <summary>
    /// 登录:用户名&密码
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    Task<AccessChain> LoginAsync(string userName, string password, string ip);

    /// <summary>
    /// 登录:用户名&密码
    /// </summary>
    /// <param name="types"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    Task<AccessChain> LoginAsync(int[] types, string userName, string password, string ip);

    /// <summary>
    /// 登录:code登录
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="type"></param>
    /// <param name="code"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    Task<AccessChain> LoginAsync(long sid, int type, string code, string ip);

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task LogoutAsync(string token);

    /// <summary>
    /// 绑定(1.成员本定社交账号,2.先有社交账户成员绑定密码成员)
    /// </summary>
    /// <param name="token"></param>
    /// <param name="openId"></param>
    /// <returns></returns>
    Task BindAsync(string token, string openId);

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    Task ChangePasswordAsync(long memberId, string oldPassword, string newPassword);

    /// <summary>
    /// 获取社交账号绑定的电话号码
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    SnsPhoneNumber GetSnsPhoneNumber(long sid, string data, string key, string iv);
}