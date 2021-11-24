﻿//----------------------------------------------------------------
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

namespace iMaxSys.Max.Domain
{
    /// <summary>
    /// 业务配置
    /// </summary>
    public class BizConfig
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        public long? XappId { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string? XappName { get; set; }

        /// <summary>
        /// TenantId
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// TenantName
        /// </summary>
        public string? TenantName { get; set; }

        /// <summary>
        /// 业务Id
        /// </summary>
        public long? BizId { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string? BizName { get; set; }
    }
}
