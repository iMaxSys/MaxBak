//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: DateTimeConverter.cs
//摘要: 日期时间转换器
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-10-30
//----------------------------------------------------------------

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace iMaxSys.Max.Json.Converters
{

    /// <summary>
    /// 日期时间转换器
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToNormalString());
        }
    }

    /// <summary>
    /// 日期时间转换器
    /// </summary>
    public class DateTimeNullableConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TryGetDateTime(out DateTime result) ? result : null;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToNormalString());
        }
    }
}