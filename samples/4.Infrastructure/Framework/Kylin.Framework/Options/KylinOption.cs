//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: KylinOption.cs
//摘要: 项目配置类
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-10-20
//----------------------------------------------------------------

using System;
using iMaxSys.Max.Options;

namespace Kylin.Framework.Options;

public class KylinOption
{
    /// <summary>
    /// 启用
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set; } = "0.0.0";

    /// <summary>
    /// 数据库配置集
    /// </summary>
    public List<DatabaseOption>? Databases { get; set; }
}