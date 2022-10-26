// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MemberService.cs
//摘要: 用户服务
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-15
//----------------------------------------------------------------

using iMaxSys.Identity;
using iMaxSys.Identity.Models;
using iMaxSys.Max.Identity.Domain;

namespace Kylin.Services.Auth;

/// <summary>
/// 用户服务
/// </summary>
public class MemberService : IMemberService
{
    /// <summary>
    /// 检查
    /// </summary>
    /// <param name="key"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task CheckAsync(string key, int type = 0)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IUser> GetAsync(long id, int type = 0)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="key"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IUser> GetAsync(string key, int type = 0)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Type GetType(int type = 0)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IUser?> LoginAsync(long userId, int type)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="registerModel"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IUser?> RegisterAsync(RegisterModel registerModel)
    {
        throw new NotImplementedException();
    }
}

