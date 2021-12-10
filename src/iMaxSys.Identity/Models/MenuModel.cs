//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MenuModel.cs
//摘要: 菜单模型
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Domain;

namespace iMaxSys.Identity.Models
{
    /// <summary>
    /// 菜单模型
    /// </summary>
    public class MenuModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Style
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// Operations
        /// </summary>
        public long[] Operations { get; set; }

        /// <summary>
        /// Router
        /// </summary>
        public string Router { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public Status? Status { get; set; }
    }
}
