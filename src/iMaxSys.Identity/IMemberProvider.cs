//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IIdentityConcreteProvider.cs
//摘要: 身份具体提供者接口
//说明: 此处本应返回IRealMember接口，但是由于反系列化不支持接口，所以暂用RealMember类返回
//      类型参数用于用户系统区别不同用户类型，不用也可
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Identity;

/// <summary>
/// 成员信息提供者接口
/// </summary>
public interface IUserProvider : IDependency
{
    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    Task<User> GetAsync(long id, int type = 0);

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="key">关键值,例如手机号码</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    Task<User> GetAsync(string key, int type = 0);

    /// <summary>
    /// 校验成员关键数据
    /// </summary>
    /// <param name="key"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    Task CheckAsync(string key, int type = 0);

    /// <summary>
    /// 激活成员
    /// </summary>
    /// <param name="key">关键值,例如手机号码</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    Task<User> ActivateAsync(string key, string avatar, int type = 0);

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    Type GetType(int type = 0);
}
