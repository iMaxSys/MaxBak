//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: OperationMode.cs
//摘要: 操作模型
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
    /// 操作模型
    /// </summary>
    public class OperationResult
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
        /// 名称
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { get; set; } = String.Empty;

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; } = String.Empty;

        /// <summary>
        /// QuickCode
        /// </summary>
        public string QuickCode { get; set; } = String.Empty;

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
        /// Router
        /// </summary>
        public string? Router { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public Status Status { get; set; } = Status.Enable;
    }
}
