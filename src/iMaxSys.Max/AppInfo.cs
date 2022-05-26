//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: BizConfig.cs
//摘要: 业务配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

namespace iMaxSys.Max
{
    /// <summary>
    /// 应用信息
    /// </summary>
    public class AppInfo
    {
        /// <summary>
        /// 综合标识
        /// </summary>
        public long Sid { get; set; }

        /// <summary>
        /// 应用Id
        /// </summary>
        public long XppId { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string XppName { get; set; } = string.Empty;

        /// <summary>
        /// TenantId
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// TenantName
        /// </summary>
        public string TenantName { get; set; } = string.Empty;

        /// <summary>
        /// 业务Id
        /// </summary>
        public long BizId { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string BizName { get; set; } = string.Empty;

        /// <summary>
        /// 成员Id
        /// </summary>
        public long MemberId { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        public string MemberName { get; set; } = string.Empty;

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}