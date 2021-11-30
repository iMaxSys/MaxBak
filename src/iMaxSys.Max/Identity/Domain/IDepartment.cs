//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IDepartment.cs
//摘要: IDepartment
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Domain;

namespace iMaxSys.Max.Identity.Domain
{
    public interface IDepartment
    {
        /// <summary>
        /// Id
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        string? Alias { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        string? Descripton { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        Status Status { get; set; }
    }
}
