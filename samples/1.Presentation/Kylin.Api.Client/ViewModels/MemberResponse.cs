﻿//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: MemberResponse.cs
//摘要: 成员应答
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-04-26
//----------------------------------------------------------------

using iMaxSys.Max.Web.Mvc;
using iMaxSys.Max.Common.Enums;

namespace Kylin.Api.Client.ViewModels;

/// <summary>
/// 会员应答
/// </summary>
public class MemberResponse : ApiResponse
{
    /// <summary>
    /// Id
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 性别 0男 1女 2未知
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    public string? Nation { get; set; }

    /// <summary>
    /// 身份证号
    /// </summary>
    public string? IdNumber { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    public string? Mobile { get; set; }

    /// <summary>
    /// 经度
    /// </summary>
    public string? Longitude { get; set; }

    /// <summary>
    /// 纬度
    /// </summary>
    public string? Latitude { get; set; }

    /// <summary>
    /// 等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 积分
    /// </summary>
    public int Points { get; set; }

    /// <summary>
    /// 消费金额
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// 是否正式
    /// </summary>
    public bool IsOfficial { get; set; }
}

