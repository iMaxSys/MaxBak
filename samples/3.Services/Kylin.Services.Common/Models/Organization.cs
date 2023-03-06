// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CompanyResult.cs
//摘要: 公司
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-10-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace Kylin.Services.Common.Models;

/// <summary>
/// 机构
/// </summary>
public class Organization
{
    /// <summary>
    /// id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 地址
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// 经度
    /// </summary>
    public string Longitude { get; set; } = string.Empty;

    /// <summary>
    /// 纬度
    /// </summary>
    public string Latitude { get; set; } = string.Empty;

    /// <summary>
    /// 电话
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// 联系人
    /// </summary>
    public string Contact { get; set; } = string.Empty;

    /// <summary>
    /// 手机号
    /// </summary>
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// 级别
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 类型:0.集团, 1.公司, 2.门店
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// 父级
    /// </summary>
    public Organization? Parent { get; set; }
}

