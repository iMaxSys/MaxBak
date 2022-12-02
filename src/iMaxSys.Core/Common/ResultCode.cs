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
}