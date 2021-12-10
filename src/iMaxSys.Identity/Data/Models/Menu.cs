//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: Menu.cs
//摘要: Menu 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Domain;
using iMaxSys.Max.Data.Entities;

namespace iMaxSys.Identity.Data.Models
{
    /// <summary>
    /// Menu
    /// </summary>
    public class Menu : TenantMasterEntity
    {
        /// <summary>
        /// XppId
        /// </summary>
        public long XppId { get; set; }

        /// <summary>
        /// 名称
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
        /// Description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Style
        /// </summary>
        public string? Style { get; set; }

        /// <summary>
        /// Router
        /// </summary>
        public string Router { get; set; } = string.Empty;

        /// <summary>
        /// 左值
        /// </summary>
        public int Lv { get; set; }

        /// <summary>
        /// 右值
        /// </summary>
        public int Rv { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        public int Deep { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Operations
        /// </summary>
        public virtual IList<Operation>? Operations { get; set; }
    }
}
