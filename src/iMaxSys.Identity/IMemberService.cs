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
using iMaxSys.Core.Models;
using iMaxSys.Identity.Models;
using iMaxSys.Sns.Common.Open;
using iMaxSys.Sns.Api;

using DbMember = iMaxSys.Identity.Data.Entities.Member;

namespace iMaxSys.Identity;

public interface IMemberService : IDependency
{
    /// <summary>
    /// 代码登录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IAccessChain> LoginAsync(CodeLoginRequest model);

    /// <summary>
    /// 密码登录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IAccessChain> LoginAsync(PasswordLoginRequest model);

    /// <summary>
    /// login user
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="userId"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    Task<IUser?> LoginUserAsync(long memberId, long userId, int type);

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="registerModel"></param>
    /// <returns></returns>
    Task<IAccessChain> RegisterAsync(RegisterRequest registerModel);

    /// <summary>
    /// RegisterUserAsync
    /// </summary>
    /// <param name="registerModel"></param>
    /// <returns></returns>
    Task<IUser?> RegisterUserAsync(RegisterRequest registerModel);

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task LogoutAsync(string token);

    /// <summary>
    /// Add member
    /// </summary>
    /// <param name="model"></param>
    /// <param name="xppId"></param>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    Task<IMember> AddAsync(MemberResult model, long xppId, long[]? roleIds);

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
    Task<IMember> GetAsync(long id);

    /// <summary>
    /// Get member
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    Task<IMember> GetAsync(string mobile);

    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="model"></param>
    /// <param name="xppId"></param>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    Task<IMember> UpdateAsync(MemberResult model, long xppId, long[]? roleIds);

    /// <summary>
    /// 获取AccessChain
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IAccessChain> GetAccessChainAsync(string token);

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
    /// <param name="xppSns"></param>
    /// <param name="memberId"></param>
    /// <param name="accessConfig"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<IAccessChain> RefreshAccessChainAsync(XppSns xppSns, long memberId, AccessConfig? accessConfig = null, IAccessToken? accessToken = null);

    /// <summary>
    /// 刷新AcceeChain缓存
    /// </summary>
    /// <param name="xppSns"></param>
    /// <param name="member"></param>
    /// <param name="accessConfig"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<IAccessChain> RefreshAccessChainAsync(XppSns xppSns, MemberResult member, AccessConfig? accessConfig = null, IAccessToken? accessToken = null);

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
    Task<SnsPhoneNumber> GetSnsPhoneNumber(long sid, string data, string key, string iv);

    /// <summary>
    /// 检查访问权限
    /// </summary>
    /// <param name="token"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    Task<IAccessChain> CheckAsync(string token, string router);
}