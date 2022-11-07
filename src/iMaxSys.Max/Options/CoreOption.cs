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
    /// UseHttpsRedirection
    /// </summary>
    public bool UseHttpsRedirection { get; set; } = true;

    /// <summary>
    /// UseStaticFiles
    /// </summary>
    public bool UseStaticFiles { get; set; } = true;

    /// <summary>
    /// UseRouting
    /// </summary>
    public bool UseRouting { get; set; } = true;

    /// <summary>
    /// UseAuthorization
    /// </summary>
    public bool UseAuthorization { get; set; } = true;

    /// <summary>
    /// Databases
    /// </summary>
    public List<DatabaseOption> Databases { get; set; } = new();
}

/// <summary>
/// 数据库选项
/// </summary>
public class DatabaseOption
{
    /// <summary>
    /// 数据库类型
    /// 0:MariaDB,1:MySQL,2:SqlServer,3:Oracle,4:Sybase
    /// 此处无法用到枚举
    /// </summary>
    public int Type { get; set; } = 0;

    /// <summary>
    /// 连接
    /// </summary>
    public string Connection { get; set; } = String.Empty;
}