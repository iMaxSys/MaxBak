//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Operation.cs
//摘要: Operation
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-01-01
//----------------------------------------------------------------

using iMaxSys.Max.Domain;

namespace iMaxSys.Max.Identity.Domain
{
    /// <summary>
    /// Operation
    /// </summary>
    public class Operation : IOperation
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// MenuId
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
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
        /// Router
        /// </summary>
        public string Router { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public Status Status { get; set; }
    }
}
