//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ICacheFactory.cs
//摘要: ICacheFactory
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using Microsoft.Extensions.Options;

using iMaxSys.Caching.Common.Enums;

namespace iMaxSys.Caching
{
    public class CacheFactory : ICacheFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MaxOption _option;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="serviceProvider"></param>
        public CacheFactory(IServiceProvider serviceProvider, IOptions<MaxOption> option)
        {
            _serviceProvider = serviceProvider;
            _option = option.Value;
        }

        public ICache GetService(CacheServer source, string connection)
        {
            return source switch
            {
                CacheServer.Redis => _serviceProvider.GetRequiredService<IRedisService>(),
                _ => _serviceProvider.GetRequiredService<IRedisService>(),
            };
        }

        public ICache GetService()
        {
            return _option.Caching.Type switch
            {
                (int)CacheServer.Redis => _serviceProvider.GetRequiredService<IRedisService>(),
                _ => _serviceProvider.GetRequiredService<IRedisService>(),
            };
        }
    }
}

