//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IdentityCache.cs
//摘要: IdentityCache
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------
/*
using System.Collections.Concurrent;

using Microsoft.Extensions.Options;

using iMaxSys.Max.Options;
using iMaxSys.Max.Caching.Redis;
using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Identity
{
    /// <summary>
    /// IdentityCache
    /// </summary>
    public class IdentityCache : RedisService, IIdentityCache
    {
        private readonly ConcurrentDictionary<long, IAuthority> _authorities;

        public IdentityCache(IOptions<MaxOption> option) : base(option.Value.Caching.Connection, option.Value.AppId)
        {
            _authorities = new ConcurrentDictionary<long, IAuthority>();
        }

        /// <summary>
        /// 租户权限字典
        /// </summary>
        public ConcurrentDictionary<long, IAuthority> Authorities
        {
            get
            {
                return _authorities;
            }
        }
    }
}
*/