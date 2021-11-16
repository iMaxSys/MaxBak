
using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using iMaxSys.Max.Domain;
using iMaxSys.Max.Options;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Caching.Redis;

namespace iMaxSys.Max.Environment
{
    /// <summary>
    /// Session
    /// </summary>
    public class Session : ISession
    {
        const string TAG_SESSION = "s:";

        private readonly ICache _cache;
        private readonly MaxOption _maxOption;

        /// <summary>
        /// SessionId
        /// </summary>
        public string Id { get; set; }

        public Session(IOptions<MaxOption> maxOption, IGenericCache cache)
        {
            _maxOption = maxOption.Value;
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>($"{TAG_SESSION}{Id}:{key}");
        }

        public async Task<T> GetAsync<T>(string key)
        {
           return await _cache.GetAsync<T>($"{TAG_SESSION}{Id}:{key}");
        }

        public void Set(string key, object data)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                throw new MaxException(ResultEnum.CantSetSession);
            }
            _cache.Set($"{TAG_SESSION}{Id}:{key}", data, DateTime.Now.AddMinutes(_maxOption.Identity.Expires));
        }

        public async Task SetAsync(string key, object data)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                throw new MaxException(ResultEnum.CantSetSession);
            }
            await _cache.SetAsync($"{TAG_SESSION}{Id}:{key}", data, DateTime.Now.AddMinutes(_maxOption.Identity.Expires));
        }
    }
}