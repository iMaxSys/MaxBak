using System;
using iMaxSys.Max.Common.Enums;

namespace Kylin.Api.ViewModels
{
    public class OperationView
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string? Alias { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// QuickCode
        /// </summary>
        public string? QuickCode { get; set; }

        /// <summary>
        /// Descripton
        /// </summary>
        public string? Descripton { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Style
        /// </summary>
        public string? Style { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        public string? Action { get; set; }

        /// <summary>
        /// IsShow
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public Status Status { get; set; }
    }
}

