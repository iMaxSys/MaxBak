//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ResultCode.cs
//摘要: 数据访问结果枚举
//说明: 102
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Data.Common;

public enum ResultCode
{
    /// <summary>
    /// 数据库连接串为空
    /// </summary>
    [Description("数据库连接串为空")]
    ConnectionIsNull = 2000,

    /// <summary>
    /// 无法获取范型仓储
    /// </summary>
    [Description("无法获取范型仓储")]
    CantGetRepository = 2001,

    /// <summary>
    /// 无法定制范型仓储
    /// </summary>
    [Description("无法获取定制仓储")]
    CantGetCustomRepository = 2002,

    /// <summary>
    /// 无效的父节点
    /// </summary>
    [Description("无效的父节点")]
    ParentIsInvalid = 2030,

    /// <summary>
    /// 无效的目标节点
    /// </summary>
    [Description("无效的目标节点")]
    TargetIsInvalid = 2031,

    /// <summary>
    /// 无效的当前节点
    /// </summary>
    [Description("无效的当前节点")]
    CurrentIsInvalid = 2032,

    /// <summary>
    /// 存在子节点
    /// </summary>
    [Description("存在子节点")]
    HasChildren = 2033,
}