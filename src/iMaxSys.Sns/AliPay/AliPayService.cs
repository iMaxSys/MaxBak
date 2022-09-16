//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: WeChatService.cs
//摘要: 微信服务
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using iMaxSys.Sns.Api;
using iMaxSys.Sns.Common.Auth;
using iMaxSys.Sns.Common.Open;

namespace iMaxSys.Sns.AliPay;

/// <summary>
/// 微信服务
/// </summary>
public class AliPayService : IAliPayService
{

    /// <summary>
    /// 构造函数
    /// </summary>
    public AliPayService()
    {
    }

    /// <summary>
    /// 获取访问配置
    /// </summary>
    /// <param name="requst"></param>
    /// <returns></returns>
    public async Task<AccessConfig> GetAccessConfigAsync(SnsAuth snsAuth)
    {
        await Task.Delay(1000);
        throw new Exception();
    }

    /// <summary>
    /// 获取电话号码
    /// </summary>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public SnsPhoneNumber GetPhoneNumber(string data, string key, string iv)
    {
        return new SnsPhoneNumber();
        //string json = AES.Decrypt(data, key, iv);
        //return JsonSerializer.Deserialize<WeChatPhoneNumber>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}