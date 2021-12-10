﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: Tenant.cs
//摘要: Tenant 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using System;

using iMaxSys.Max.Domain;
using iMaxSys.Max.Data.Entities;

namespace iMaxSys.Data.Models
{
    /// <summary>
    /// Tenant
    /// </summary>
    public class Tenant : MasterEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; } = string.Empty;

        /// <summary>
        /// 速查码
        /// </summary>
        public string QuickCode { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Start
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// End
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; } = Status.Enable;
    }
}
