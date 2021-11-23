//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: LongConverter.cs
//摘要: long转换器
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
    /// long转换器
    /// </summary>
    public class LongConverter : JsonConverter<long>
    {
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetInt64();
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    /// <summary>
    /// long转换器
    /// </summary>
    public class LongNullableConverter : JsonConverter<long?>
    {
        public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TryGetInt64(out long result) ? result : null;
        }

        public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}
