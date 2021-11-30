
using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using iMaxSys.Max.Domain;
using iMaxSys.Max.Options;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Caching.Redis;

namespace iMaxSys.Max.Environment.Access
{
    /// <summary>
    /// Session
    /// </summary>
    public class Session : ISession
    {
        private readonly ISessionStore _sessionStore;
        private readonly string _id = "";

        /// <summary>
        /// SessionId
        /// </summary>
        public string Id { get => _id; }

        public Session(ISessionStore sessionStore)
        {
            _sessionStore = sessionStore;
            _id = _sessionStore.Id;
        }

        public T? Get<T>(string key)
        {
            return _sessionStore.Get<T>(key);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
           return await _sessionStore.GetAsync<T>(key);
        }

        public void Set(string key, object data)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                throw new MaxException(ResultEnum.CantSetSession);
            }
            _sessionStore.Set(key, data);
        }

        public async Task SetAsync(string key, object data)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                throw new MaxException(ResultEnum.CantSetSession);
            }
            await _sessionStore.SetAsync(key, data);
        }

        public void Clear()
        {
            _sessionStore.Clear();
        }

        /// <summary>
        /// 清除指定key
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _sessionStore.Remove(key);
        }
    }
}