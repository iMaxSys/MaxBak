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
    /// 版本
    /// </summary>
    public string Notice { get; set; } = "hello world.";

    /// <summary>
    /// 数据库配置集
    /// </summary>
    public List<DatabaseOption>? Databases { get; set; }
}