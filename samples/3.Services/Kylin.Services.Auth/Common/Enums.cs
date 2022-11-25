// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: UserType.cs
//摘要: 用户类型
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-15
//----------------------------------------------------------------

using System.ComponentModel;

namespace Kylin.Services.Auth.Common;

/// <summary>
/// 用户类型
/// </summary>
public enum UserType
{
    /// <summary>
    /// 顾客
    /// </summary>
    [Description("顾客")]
    Customer = 0,

    /// <summary>
    /// 职员
    /// </summary>
    [Description("职员")]
    Staff = 1,

    /// <summary>
    /// 教练
    /// </summary>
    [Description("教练")]
    Coach = 2,

    /// <summary>
    /// 管理员
    /// </summary>
    [Description("管理员")]
     Administrator= 9,
}

