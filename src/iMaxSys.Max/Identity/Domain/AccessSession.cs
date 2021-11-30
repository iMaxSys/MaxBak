//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AccessSession.cs
//摘要: 访问令牌Session
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System;

namespace iMaxSys.Max.Identity.Domain
{
    /// <summary>
    /// 访问令牌Session
    /// </summary>
    public class AccessSession : IAccessSession
    {
        /// <summary>
        /// XappSnsId
        /// </summary>
        public long XappSnsId { get; set; }

        /// <summary>
        /// 来源Id/应用来源
        /// </summary>
        public long AppSource { get; set; }

        /// <summary>
        /// 账号来源/社交来源
        /// </summary>
        public int AccountSource { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long? MemberId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// 第三方平台原始Id
        /// </summary>
        public string? AccountId { get; set; }

        /// <summary>
        /// 第三方平台App
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// 第三方平台Id
        /// </summary>
        public string? OpenId { get; set; }

        /// <summary>
        /// SessionKey
        /// </summary>
        public string? SessionKey { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 第三方平台统一Id
        /// </summary>
        public string? UnionId { get; set; }

        /// <summary>
        /// Token过期时间
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// 是否是正式用户
        /// </summary>
        public bool IsOfficial { get; set; }

        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
