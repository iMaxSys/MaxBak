
//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Role.cs
//摘要: Role
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-01-01
//----------------------------------------------------------------

using System;
using System.Collections.Generic;

using iMaxSys.Max.Domain;

namespace iMaxSys.Max.Identity.Domain
{
    /// <summary>
    /// Role
    /// </summary>
    public class Role : IRole
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// TenantId
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripton
        /// </summary>
        public string Descripton { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Style
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// Menus("45675,45677")
        /// </summary>
        public long[] MenuIds { get; set; }

        /// <summary>
        /// Operations("45675,45677")
        /// </summary>
        public long[] OperationIds { get; set; }

        /// <summary>
        /// Start
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// End
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Menu
        /// </summary>
        public IMenu Menu { get; set; }
    }
}
