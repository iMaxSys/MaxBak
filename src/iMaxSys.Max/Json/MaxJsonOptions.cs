//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MaxJsonOptions.cs
//摘要: MaxJsonOptions
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using Microsoft.Extensions.Options;

namespace iMaxSys.Max.Json;

/// <summary>
/// MaxJsonOptions
/// </summary>
public static class MaxJsonOptions
{
    public static JsonSerializerOptions JsonSerializerOptions { get; }

    static MaxJsonOptions()
    {
        JsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        Configure(JsonSerializerOptions);
    }

    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="options"></param>
    public static void Configure(JsonSerializerOptions options)
    {

        options.Converters.Add(new Json.Converters.LongConverter());
        options.Converters.Add(new Json.Converters.LongNullableConverter());
        options.Converters.Add(new Json.Converters.DateTimeConverter());
        options.Converters.Add(new Json.Converters.DateTimeNullableConverter());
        options.WriteIndented = false;                                                  //
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;                      //小驼峰, 蛇形: new SnakeCaseNamingPolicy();
        options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;                       //字典键小驼峰
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;           //忽略空值属性
        options.PropertyNameCaseInsensitive = true;                                     //不区分大小写
        options.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        options.Encoder = JavaScriptEncoder.Create(allowedRanges: UnicodeRanges.All);   //序列化语言字符集
        //options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }
}
