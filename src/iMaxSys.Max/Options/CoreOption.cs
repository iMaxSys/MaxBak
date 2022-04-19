//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CoreOption.cs
//摘要: CoreOption
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Options;

/// <summary>
/// 核心业务选项
/// </summary>
public class CoreOption
{
    /// <summary>
    /// Master
    /// </summary>
    public DatabaseOption Master { get; set; } = new();

    /// <summary>
    /// Slave
    /// </summary>
    public DatabaseOption? Slave { get; set; }
}

/// <summary>
/// 数据库选项
/// </summary>
public class DatabaseOption
{
    /// <summary>
    /// 数据库类型
    /// 0:MariaDB,1:MySQL,2:SqlServer,3:Oracle,4:Sybase
    /// </summary>
    public int Type { get; set; } = 0;

    /// <summary>
    /// 连接
    /// </summary>
    public string Connection { get; set; } = String.Empty;
}