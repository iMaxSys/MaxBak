//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MaxOption.cs
//摘要: 框架选项
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Options
{
    /// <summary>
    /// 框架选项
    /// </summary>
    public class MaxOption
    {
        /// <summary>
        /// 应用标识
        /// </summary>
        public long AppId { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 应用版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 核心
        /// </summary>
        public CoreOption Core { get; set; }

        /// <summary>
        /// 网络
        /// </summary>
        public NetworkOption Network { get; set; }

        /// <summary>
        /// 缓存
        /// </summary>
        public CachingOption Caching { get; set; }

        /// <summary>
        /// 身份认证
        /// </summary>
        public IdentityOption Identity { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public MessageOption Message { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        public LoggingOption Logging { get; set; }
    }
}
