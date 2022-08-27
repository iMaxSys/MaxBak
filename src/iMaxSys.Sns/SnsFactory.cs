//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: SnsFactory.cs
//摘要: 社交服务工厂
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using iMaxSys.Sns.Common;
using iMaxSys.Sns.WeChat;
using iMaxSys.Sns.AliPay;
using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Sns;

/// <summary>
/// SnsFactory
/// </summary>
public class SnsFactory : ISnsFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="serviceProvider"></param>
    public SnsFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 获取社交服务
    /// </summary>
    /// <param name="platform"></param>
    /// <returns></returns>
    public ISns GetService(SnsSource snsSource)
    {
        return snsSource switch
        {
            SnsSource.WeChat => _serviceProvider.GetRequiredService<IWeChatService>(),
            SnsSource.AliPay => _serviceProvider.GetRequiredService<IAliPayService>(),
            _ => _serviceProvider.GetRequiredService<IWeChatService>(),
        };
    }
}