//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ISnsService.cs
//摘要: 社交服务接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using iMaxSys.Sns.Api;
using iMaxSys.Sns.Common.Auth;
using iMaxSys.Sns.Common.Open;

namespace iMaxSys.Sns;

/// <summary>
/// 社交服务接口
/// </summary>
public interface ISns
{
    /// <summary>
    /// 登录获取访问配置
    /// </summary>
    /// <param name="snsAuth"></param>
    /// <returns></returns>
    Task<AccessConfig> LoginAsync(SnsAuth snsAuth);

    /// <summary>
    /// 获取电话号码
    /// </summary>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    SnsPhoneNumber GetPhoneNumber(string data, string key, string iv);
}