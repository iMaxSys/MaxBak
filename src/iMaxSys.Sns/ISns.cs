﻿//----------------------------------------------------------------
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

using System.Threading.Tasks;

using iMaxSys.SDK.Sns.Api;
using iMaxSys.SDK.Sns.Domain.Auth;
using iMaxSys.SDK.Sns.Domain.Open;

namespace iMaxSys.SDK.Sns
{
    /// <summary>
    /// 社交服务接口
    /// </summary>
    public interface ISns
    {
        /// <summary>
        /// 获取访问配额
        /// </summary>
        /// <param name="snsAuth"></param>
        /// <returns></returns>
        Task<AccessConfig> GetAccessConfigAsync(SnsAuth snsAuth);

        /// <summary>
        /// 获取电话号码
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        SnsPhoneNumber GetPhoneNumber(string data, string key, string iv);
    }
}