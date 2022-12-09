//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Enums.cs
//摘要: 枚举
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-04-19
//----------------------------------------------------------------

namespace iMaxSys.Core.Common;

/// <summary>
/// DbServer[0:MariaDB,1:MySQL,2:SqlServer,3:Oracle,4:PostgreSQL]
/// </summary>
public enum ResultCode
{
    /// <summary>
    /// 无效的应用
    /// </summary>
    [Description("无效的应用")]
    XppIsInvalid = 103000,

    /// <summary>
    /// 无效的应用账号
    /// </summary>
    [Description("无效的应用账号")]
    XppSnsIsInvalid = 103001,

    /// <summary>
    /// 字典已存在
    /// </summary>
    [Description("字典已存在")]
    DictExists = 103100,

    /// <summary>
    /// 字典不可删除
    /// </summary>
    [Description("字典不可删除")]
    DictCantRemove = 103101,

    /// <summary>
    /// 字典存在字典项不可删除
    /// </summary>
    [Description("字典存在字典项, 不可删除")]
    DictHasItem = 103102,

    /// <summary>
    /// 字典不可修改
    /// </summary>
    [Description("字典不可修改")]
    DictCantUpdate = 103103,

    /// <summary>
    /// 字典项名称已存在
    /// </summary>
    [Description("字典项名称已存在")]
    DictItemExists = 103104,

    /// <summary>
    /// 字典项不可删除
    /// </summary>
    [Description("字典项不可删除")]
    DictItemCantRemove = 103105,

    /// <summary>
    /// 字典项不可修改
    /// </summary>
    [Description("字典项不可修改")]
    DictItemCantUpdate = 103106,
}