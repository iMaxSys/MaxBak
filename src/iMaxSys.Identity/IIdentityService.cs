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
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity.Models;

namespace iMaxSys.Identity;

/// <summary>
/// 成员服务接口
/// </summary>
public interface IIdentityService : IDependency
{
    /// <summary>
    /// 获取访问钥匙串
    /// </summary>
    /// <param name="token">令牌</param>
    /// <returns></returns>
    Task<IAccessChain> GetAsync(string token);

    /// <summary>
    /// API权限检查
    /// </summary>
    /// <param name="token"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    Task<IAccessChain> CheckAsync(string token, string router);

    /// <summary>
    /// 校验用户关键数据是否合法
    /// </summary>
    /// <param name="key"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    Task CheckKeyAsync(string key, int type);

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IMember> GetMemberAsync(long id);

    /// <summary>
    /// 获取用户关键值
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sids"></param>
    /// <returns></returns>
    Task<Dictionary<long, string>> GetKeysAsync(long id, long[]? sids = null);

    /// <summary>
    /// 获取用户关键值
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <param name="sids"></param>
    /// <returns></returns>
    Task<Dictionary<long, string>> GetKeysAsync(long id, int type, long[]? sids = null);

    /// <summary>
    /// 新增成员
    /// </summary>
    /// <param name="model"></param>
    /// <param name="ip"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    Task<IMember> AddMemberAsync(MemberModel member, string ip, long tenantId = 0);

    /// <summary>
    /// 更新成员
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IMember> UpdateMemberAsync(MemberModel member);

    /// <summary>
    /// 移除成员
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveMemberAsync(long id);

    /// <summary>
    /// 获取成员角色
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IRole> GetMemberRoleAsync(long id);

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="accessChain"></param>
    Task<IRole> GetMemberRoleAsync(IAccessChain accessChain);

    /// <summary>
    /// 获取成员菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IMenu> GetMemberMenuAsync(long id);

    /// <summary>
    /// 获取完整菜单
    /// </summary>
    /// <param name="id">租户Id</param>
    /// <returns></returns>
    Task<IMenu> GetFullMenuAsync(long id);

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