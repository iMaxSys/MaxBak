//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: WorkContext.cs
//摘要: WorkContext
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Max.Environment
{
    /// <summary>
    /// WorkContext
    /// </summary>
    public class WorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplication _application;
        private readonly ISession _session;

        private IAccessChain _accessChain;

        /// <summary>
        /// 访问链
        /// </summary>
        public IAccessChain AccessChain
        {
            get
            {
                return _accessChain;
            }
            set
            {
                _accessChain = value;
                _session.Id = value?.AccessSession?.Token;
            }
        }

        /// <summary>
        /// Application
        /// </summary>
        public IApplication Application { get => _application; }

        /// <summary>
        /// 会话
        /// </summary>
        public ISession Session { get => _session; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        public WorkContext()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="session"></param>
        public WorkContext(IHttpContextAccessor httpContextAccessor, IApplication application, ISession session)
        {
            _httpContextAccessor = httpContextAccessor;
            _application = application;
            _session = session;
            IP = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress.ToString();
        }

        /// <summary>
        /// Get service of type
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            var provider = _httpContextAccessor?.HttpContext?.RequestServices;
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return (T)provider.GetService(typeof(T));
        }

        /// <summary>
        /// Get service of type
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <returns></returns>
        public T GetRequiredService<T>()
        {
            var provider = _httpContextAccessor?.HttpContext?.RequestServices;
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return (T)provider.GetRequiredService(typeof(T));
        }
    }
}