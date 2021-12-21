//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: EnumExtention.cs
//摘要: EnumExtention
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Extentions;

/// <summary>
/// 枚举扩展
/// </summary>
public static class EnumExtensions
{
    private static ConcurrentDictionary<Enum, string> _concurrentDictionary = new ConcurrentDictionary<Enum, string>();

    /// <summary>
    /// 获取枚举的描述信息(Descripion)。
    /// 支持位域，如果是位域组合值，多个按分隔符组合。
    /// </summary>
    public static string GetDescription(this Enum value)
    {
        return _concurrentDictionary.GetOrAdd(value, (key) =>
        {
            var type = key.GetType();
            var field = type.GetField(key.ToString());
                //如果field为null则应该是组合位域值，
                return field == null ? key.GetDescriptions() : GetDescription(field);
        });
    }

    /// <summary>
    /// 获取系统结果
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result Result(this Enum value)
    {
        return new Result
        {
            Success = value.GetHashCode() == ResultEnum.Success.GetHashCode(),
            Code = value.GetHashCode(),
            Message = GetDescription(value),
            Data = null
        };
    }

    /// <summary>
    /// 获取系统结果
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result Result(this Enum value, object data)
    {
        return new Result
        {
            Success = value.GetHashCode() == ResultEnum.Success.GetHashCode(),
            Code = value.GetHashCode(),
            Message = GetDescription(value),
            Data = data
        };
    }

    /// <summary>
    /// 获取系统结果
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result<T> Result<T>(this Enum value, T data)
    {
        return new Result<T>
        {
            Success = value.GetHashCode() == ResultEnum.Success.GetHashCode(),
            Code = value.GetHashCode(),
            Message = GetDescription(value),
            Data = data
        };
    }

    /// <summary>
    /// 获取位域枚举的描述，多个按分隔符组合
    /// </summary>
    public static string GetDescriptions(this Enum value, string separator = ",")
    {
        var names = value.ToString().Split(',');
        string[] res = new string[names.Length];
        var type = value.GetType();
        for (int i = 0; i < names.Length; i++)
        {
            var field = type.GetField(names[i].Trim());
            if (field == null) continue;
            res[i] = GetDescription(field);
        }
        return string.Join(separator, res);
    }

    private static string GetDescription(FieldInfo field)
    {
        var att = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
        return att == null ? field.Name : ((DescriptionAttribute)att).Description;
    }
}