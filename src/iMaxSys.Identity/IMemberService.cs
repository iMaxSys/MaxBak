﻿//----------------------------------------------------------------
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
using iMaxSys.Data.Entities.App;
using iMaxSys.Sns.Api;

using DbMember = iMaxSys.Identity.Data.Entities.Member;
using iMaxSys.Identity.Models.Request;

namespace iMaxSys.Identity;

public interface IMemberService : IDependency
{
    /// <summary>
    /// 扫码登录
    /// </summary>
    /// <param name="xppSnsId"></param>
    /// <param name="code"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    //Task<IAccessChain> LoginAsync(long xppSnsId, string code, string ip);

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="xppSnsId"></param>
    /// <param name="type"></param>
    /// <param name="code"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    Task<IAccessChain> LoginAsync(CodeLoginRequest request);

    /// <summary>
    /// login
    /// </summary>
    /// <param name="xppSnsId"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    Task<IAccessChain> LoginAsync(long xppSnsId, int type, string userName, string password, string ip);

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
    Task<IAccessChain> RegisterAsync(RegisterModel registerModel);

    /// <summary>
    /// RegisterUserAsync
    /// </summary>
    /// <param name="registerModel"></param>
    /// <returns></returns>
    Task<IUser?> RegisterUserAsync(RegisterModel registerModel);

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
    Task<IMember> AddAsync(MemberModel model, long xppId, long[]? roleIds);

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
    /// <param name="xppId"></param>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    Task<IMember> UpdateAsync(MemberModel model, long xppId, long[]? roleIds);

    /// <summary>
    /// 获取AccessChain
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IAccessChain?> GetAccessChainAsync(string token);

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
    Task<IAccessChain> RefreshAccessChainAsync(XppSns xppSns, DbMember member, AccessConfig? accessConfig = null, IAccessToken? accessToken = null);

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
}