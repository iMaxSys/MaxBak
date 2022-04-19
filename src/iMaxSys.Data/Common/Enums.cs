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

namespace iMaxSys.Data.Common.Enums;

/// <summary>
/// DbServer[0:MariaDB,1:MySQL,2:SqlServer,3:Oracle,4:PostgreSQL]
/// </summary>
public enum DbServer
{
    /// <summary>
    /// MariaDB
    /// </summary>
    [Description("MariaDB")]
    MariaDB = 0,
    /// <summary>
    /// MariaDB
    /// </summary>
    [Description("MySQL")]
    MySQL = 1,
    /// <summary>
    /// MariaDB
    /// </summary>
    [Description("SqlServer")]
    SqlServer = 2,
    /// <summary>
    /// MariaDB
    /// </summary>
    [Description("Oracle")]
    Oracle = 3,
    /// <summary>
    /// MariaDB
    /// </summary>
    [Description("PostgreSQL")]
    PostgreSQL = 4
}