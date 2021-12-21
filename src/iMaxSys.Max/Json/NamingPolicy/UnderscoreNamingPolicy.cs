//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: UnderscoreNamesContractResolver.cs
//摘要: UnderscoreNamesContractResolver
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Json.NamingPolicy;

/// <summary>
/// 下划线命名策略
/// </summary>
public class UnderscoreNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        string n = "";
        try
        {
            n = Regex.Replace(name, @"((?<=.)[A-Z][a-z]*)", @"_$1").ToLower();
        }
        catch
        {
        }
        return n;
    }
}

/// <summary>
/// 蛇形/下划线命名策略
/// </summary>
public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
    }
}