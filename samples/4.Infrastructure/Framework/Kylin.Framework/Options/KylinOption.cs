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