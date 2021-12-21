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

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Identity.Domain;

public class Department : IDepartment
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
    /// 描述
    /// </summary>
    public string? Descripton { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; }
}