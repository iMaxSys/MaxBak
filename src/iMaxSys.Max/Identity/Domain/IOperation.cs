//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IOperation.cs
//摘要: IOperation
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
    /// IOperation
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Id
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// MenuId
        /// </summary>
        long MenuId { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Descripton
        /// </summary>
        string Descripton { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        string Icon { get; set; }

        /// <summary>
        /// Style
        /// </summary>
        string Style { get; set; }

        /// <summary>
        /// Router
        /// </summary>
        string Router { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        Status Status { get; set; }
    }
}