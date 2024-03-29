﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IUserProvider.cs
//摘要: 身份具体提供者接口
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
/// 成员信息提供者接口
/// </summary>
public interface IUserService : IDependency
{
    /// <summary>
    /// 获取用户
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    Task<IUser?> GetAsync(long id, int type = 0);

    /// <summary>
    /// 获取用户
    /// </summary>
    /// <param name="key">关键值,例如手机号码</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    Task<IUser?> GetAsync(string key, int type = 0);

    /// <summary>
    /// 校验成员关键数据
    /// </summary>
    /// <param name="key"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    Task CheckAsync(string key, int type = 0);

    /// <summary>
    /// register
    /// </summary>
    /// <param name="registerModel"></param>
    /// <returns></returns>
    Task<IUser?> RegisterAsync(RegisterRequest registerModel);

    /// <summary>
    /// login
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    Task<IUser?> LoginAsync(long userId, int type);

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    Type GetType(int type = 0);
}