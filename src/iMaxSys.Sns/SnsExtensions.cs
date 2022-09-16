//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: SnsExtensions.cs
//摘要: SnsExtensions
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-20
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;
using iMaxSys.Sns.Common;
using iMaxSys.Sns.WeChat;

namespace iMaxSys.Sns;

/// <summary>
/// 社交服务解析器
/// </summary>
/// <param name="source"></param>
/// <returns></returns>
public delegate ISns SnsResolver(SnsSource source);

/// <summary>
/// 社交扩展
/// </summary>
public static class SnsExtensions
{
    /// <summary>
    /// AddSns
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="KeyNotFoundException"></exception>
    public static void AddSns(this IServiceCollection services, IConfiguration configuration)
    {
        //也可以在ISns上标识IDependency达到注册的效果
        services.AddScoped<ISns, WeChatService>();

        services.AddScoped<SnsResolver>(serviceProvider => source =>
        {
            return source switch
            {
                SnsSource.WeChat => serviceProvider.GetRequiredService<IWeChatService>(),
                _ => throw new KeyNotFoundException(),
            };
        });
    }
}