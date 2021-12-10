
using System;
using System.Collections.Generic;

using iMaxSys.Max.Domain;
using iMaxSys.Max.Data.Entities;

namespace iMaxSys.Identity.Data.Models
{
    /// <summary>
    /// Role
    /// </summary>
    public class Role : TenantMasterEntity
    {
        /// <summary>
        /// XppId
        /// </summary>
        public long XppId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; } = string.Empty;

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// QuickCode
        /// </summary>
        public string QuickCode { get; set; } = string.Empty;

        /// <summary>
        /// Descripton
        /// </summary>
        public string? Descripton { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Style
        /// </summary>
        public string? Style { get; set; }

        /// <summary>
        /// Menus("45675,45677")
        /// </summary>
        public string? MenuIds { get; set; }

        /// <summary>
        /// Operations("45675,45677")
        /// </summary>
        public string? OperationIds { get; set; }

        /// <summary>
        /// Start
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// End
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Members
        /// </summary>
        public virtual IList<RoleMember>? RoleMembers { get; set; }
    }
}