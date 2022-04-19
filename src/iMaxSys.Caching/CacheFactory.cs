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

using iMaxSys.Caching.Common;

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
        public CacheFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CacheFactory(IServiceProvider serviceProvider, IOptions<MaxOption> option)
        {
            _serviceProvider = serviceProvider;
            _option = option.Value;
        }

        public ICache GetService(CacheSource source, string connection)
        {
            return source switch
            {
                CacheSource.Redis => _serviceProvider.GetRequiredService<IRedisService>(),
                _ => _serviceProvider.GetRequiredService<IRedisService>(),
            };
        }

        public ICache GetService()
        {
            return _option.Caching. switch
            {
                CacheSource.Redis => _serviceProvider.GetRequiredService<IRedisService>(),
                _ => _serviceProvider.GetRequiredService<IRedisService>(),
            };
        }
    }
}

