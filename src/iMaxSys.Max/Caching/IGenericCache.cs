using System;

using iMaxSys.Max.Caching.Redis;
using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Caching
{
    public interface IGenericCache : ICache, ISingleton
    {
    }
}
