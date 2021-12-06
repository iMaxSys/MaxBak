//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: UtilityExtention.cs
//摘要: UtilityExtention
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Collection;
using iMaxSys.Max.Domain;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;

namespace iMaxSys.Max.Extentions
{
    /// <summary>
    /// 工具扩展
    /// </summary>
    public static class UtilityExtentions
    {
        const string FORMAT_DATE = "yyyy-MM-dd";
        const string FORMAT_CHNDATE = "yyyy年MM月dd日";
        const string FORMAT_TIME = "yyyy-MM-dd HH:mm:ss";
        const string FORMAT_SHORTTIME = "yy-MM-dd HH:mm:ss";
        const string FORMAT_LONGTIME = "yyyy-MM-dd HH:mm:ss.fff";
        const string FORMAT_DATESHORTTIME = "yyyy-MM-dd HH:mm";
        const string FORMAT_ONLYTIME = "HH:mm";
        const string FORMAT_MONTH = "yyyy-MM";
        const string SEPARATER = ",";

        public static DateTime StartTimePoint;

        static UtilityExtentions()
        {
            StartTimePoint = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
        }

        /// <summary>
        /// 整型位开关(11001->3=true)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index">位数(from 0)</param>
        /// <returns></returns>
        public static bool IndexEnable(this int value, int index)
        {
            return (value / (int)Math.Pow(10, index)) % 10 == 1;
        }

        /// <summary>
        /// 整型位开关(11001->3=true)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index">位数(from 0)</param>
        /// <returns></returns>
        public static bool IndexEnable(this int? value, int index)
        {
            return value == null || (value.Value / (int)Math.Pow(10, index)) % 10 == 1;
        }

        /// <summary>
        /// 位值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index">位数(from 0)</param>
        /// <returns></returns>
        public static int IndexValue(this long value, int index)
        {
            return (int)((value / (int)Math.Pow(10, index)) % 10);
        }

        public static string ToIntMoneyString(this decimal value)
        {
            return value.ToString("f0");
        }

        public static string ToMoneyString(this decimal value)
        {
            return value.ToString("f2");
        }

        public static string ToDiscountString(this decimal value)
        {
            return value.ToString("f1");
        }

        public static string? ToIntMoneyString(this decimal? value)
        {
            return value?.ToString("f0");
        }

        public static string? ToMoneyString(this decimal? value)
        {
            return value?.ToString("f2");
        }

        /// <summary>
        /// 格式化普通格式日期字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_DATE);
        }

        /// <summary>
        /// 格式化中文日期字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToChnDateString(this DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_CHNDATE);
        }

        /// <summary>
        /// 格式化普通格式时间字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToNormalString(this DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_TIME);
        }

        public static string? ToNormalString(this DateTime? dateTime)
        {
            return dateTime?.ToString(FORMAT_TIME);
        }

        /// <summary>
        /// 格式化长格式时间字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToLongString(this DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_LONGTIME);
        }

        /// <summary>
        /// 格式化短格式时间字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToShortString(this DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_SHORTTIME);
        }

        /// <summary>
        /// 格式化为日期时分时间字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateShortTimeString(this DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_DATESHORTTIME);
        }

        // <summary>
        /// 格式化为日期时分时间字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string? ToDateShortTimeString(this DateTime? dateTime)
        {
            return dateTime?.ToString(FORMAT_DATESHORTTIME);
        }

        /// <summary>
        /// 格式化时间字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_ONLYTIME);
        }

        /// <summary>
        /// 格式化年月时间字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToMonthString(this DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_MONTH);
        }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            return (long)((dateTime - StartTimePoint).TotalSeconds);
        }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToLongTimestamp(this DateTime dateTime)
        {
            return (long)((dateTime - StartTimePoint).TotalMilliseconds);
        }

        /// <summary>
        /// 获取占用时间
        /// </summary>
        /// <param name="dateTime">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public static TakeTime ToTakeTime(this DateTime start, DateTime? end = null)
        {
            if (end.HasValue && end.Value.Hour >= 12 && start.Hour < 12)
            {
                return TakeTime.AllDay;
            }
            else
            {
                return start.Hour < 12 ? TakeTime.AM : TakeTime.PM;
            }
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long number)
        {
            return StartTimePoint.AddMilliseconds(number).ToLocalTime();
        }

        /// <summary>
        /// 获取时间根据字符串
        /// </summary>
        public static DateTime StrToDatetime(this string dateTime)
        {
            return DateTime.Parse(dateTime);
        }

        /// <summary>
        /// 获取当前周
        /// </summary>
        public static string ToWeek(this DateTime dateTime)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);
        }

        /// <summary>
        /// 获取当前周
        /// </summary>
        public static string ToChnWeek(this DateTime dateTime, string prefix)
        {
            string[] Day = new string[] { "日", "一", "二", "三", "四", "五", "六" };
            return $"{prefix}{Day[(int)dateTime.DayOfWeek]}";
        }

        /// <summary>
        /// 获取本月第一天
        /// </summary>
        public static DateTime ToMonthFirstDay(this DateTime dateTime)
        {
            return dateTime.AddDays(1 - dateTime.Day).Date;
        }

        /// <summary>
        /// 获取本月最后一天
        /// </summary>
        public static DateTime ToMonthLastDay(this DateTime dateTime)
        {
            return dateTime.ToMonthFirstDay().AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取本年第一天
        /// </summary>
        public static DateTime ToYearFirstDay(this DateTime dateTime)
        {
            return dateTime.AddMonths(1 - dateTime.Month).AddDays(1 - dateTime.Day).Date;
        }

        /// <summary>
        /// 获取本月最后一天
        /// </summary>
        public static DateTime ToYearLastDay(this DateTime dateTime)
        {
            return dateTime.ToYearFirstDay().AddYears(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取字符串左起指定长度字符串
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string Left(this string source, int length)
        {
            return source[..Math.Min(source.Length, length)];
        }

        /// <summary>
        /// 获取字符串右起指定长度字符串
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string Right(this string source, int length)
        {
            return source[^length..];
        }

        /// <summary>
        /// 转为int数组
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int[] ToIntArray(this string source)
        {
            return Array.ConvertAll<string, int>(source.Split(SEPARATER), int.Parse);
        }

        /// <summary>
        /// 转为long数组
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long[] ToLongArray(this string source)
        {
            return Array.ConvertAll<string, long>(source.Split(SEPARATER), long.Parse);
        }

        /// <summary>
        /// 转为字符串数组
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this string source)
        {
            return source.Split(SEPARATER);
        }

        /// <summary>
        /// 转为字典(from->k1:v1,k2:v2)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Dictionary<string, string>? ToStringDictionary(this string source)
        {
            return string.IsNullOrWhiteSpace(source) ? null : source.Split(SEPARATER).Select(x => x.Split(":")).ToDictionary(x => x[0], x => x.Count() > 1 ? x[1] : x[0]);
        }

        #region Separater String Left Right

        private static string GetSeparaterLeft(string source, string separater)
        {
            return source.IndexOf(separater) > 0 ? source.Substring(0, source.IndexOf(separater)) : source;
        }

        private static string GetSeparaterRight(string source, string separater)
        {
            return source.IndexOf(separater) > 0 ? source.Substring(source.IndexOf(separater) + separater.Length, source.Length - source.IndexOf(separater) - separater.Length) : source;
        }

        /// <summary>
        /// 获取分隔符左边的字符串
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns></returns>
        public static string Left(this string source)
        {
            return GetSeparaterLeft(source, SEPARATER);
        }

        /// <summary>
        /// 获取分隔符左边的字符串
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="separater">分隔符</param>
        /// <returns></returns>
        public static string Left(this string source, string separater)
        {
            return GetSeparaterLeft(source, separater);
        }

        /// <summary>
        /// 获取分隔符右边的字符串
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns></returns>
        public static string Right(this string source)
        {
            return GetSeparaterRight(source, SEPARATER);
        }

        /// <summary>
        /// 获取分隔符右边的字符串
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="separater">分隔符</param>
        /// <returns></returns>
        public static string Right(this string source, string separater)
        {
            return GetSeparaterRight(source, separater);
        }

        #endregion

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncodeBase64(this string source)
        {
            string encode = "";
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DecodeBase64(this string source)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(source);
            try
            {
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                decode = source;
            }
            return decode;
        }

        /// <summary>
        /// 转为可为空字符串(如果为空白字符则为null)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string? ToNullable(this string source)
        {
            return string.IsNullOrWhiteSpace(source) ? null : source;
        }

        /// <summary>
        /// 指示指定的 <see cref="System.String"/> 对象是 null 还是 System.String.Empty 字符串。
        /// </summary>
        public static bool IsNullOrEmpty(this String data)
        {
            return String.IsNullOrEmpty(data);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        public static bool IsNullOrWhiteSpace(this String data)
        {
            return String.IsNullOrWhiteSpace(data);
        }

        /// <summary>
        /// 	如果指定的 <see cref="System.String"/> 对象字符串是 null、空还是仅由空白字符组成则返回默认值。
        /// </summary>
        public static string IfNullOrWhiteSpace(this string value, string defaultValue)
        {
            return (!value.IsNullOrWhiteSpace() ? value : defaultValue);
        }

        /// <summary>
        /// 如果指定的 <see cref="System.String"/> 对象是 null 或 System.Empty 字符串则返回默认值。
        /// </summary>
        public static string IfNullOrEmpty(this string value, string defaultValue)
        {
            return (!value.IsNullOrEmpty() ? value : defaultValue);
        }

        /// <summary>
        /// 序列化为Json字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="camelCase">是否为小驼峰格式</param>
        /// <returns></returns>
        public static string ToJson(this object value, bool camelCase = true)
        {
            var serializeOptions = new JsonSerializerOptions();

            if (camelCase)
            {
                serializeOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            };
            serializeOptions.Converters.Add(new Json.Converters.DateTimeConverter());
            serializeOptions.Converters.Add(new Json.Converters.DateTimeNullableConverter());
            serializeOptions.Converters.Add(new Json.Converters.LongConverter());
            serializeOptions.Converters.Add(new Json.Converters.LongNullableConverter());
            serializeOptions.PropertyNameCaseInsensitive = true;
            serializeOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(allowedRanges: UnicodeRanges.All);
            return JsonSerializer.Serialize(value, serializeOptions);
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetFileNameExtension(this string value)
        {
            return value.Substring(value.LastIndexOf(".") + 1);
        }

        /// <summary>
        /// 获取应用相关程序集(为提高性能,只加载框架相关程序集和应用程序程序集)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAppAssemblies()
        {
            string appName = AppDomain.CurrentDomain.FriendlyName.Left(".");
            string fxName = Assembly.GetExecutingAssembly().FullName.Left(".");
            return DependencyContext.Default.CompileLibraries.Where(c => c.Name.StartsWith(appName, StringComparison.CurrentCultureIgnoreCase) || c.Name.StartsWith(fxName, StringComparison.CurrentCultureIgnoreCase)).Select(x => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(x.Name)));
        }

        /// <summary>
        /// 获取应用相关实现类型
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetAppTypes()
        {
            return GetAppAssemblies().SelectMany(x => x.GetTypes()).Where(c => c.IsClass && !c.IsAbstract);
        }

        /// <summary>
        /// 字符串转化为Int
        /// </summary>
        public static int ToInt(this string source)
        {
            int result;
            int.TryParse(source, out result);
            return result;
        }

        /// <summary>
        /// 字符串转化为Long
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns></returns>
        public static long ToLong(this string source)
        {
            long result;
            long.TryParse(source, out result);
            return result;
        }

        /// <summary>
        /// 字符串转化为Decimal
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string source)
        {
            decimal result;
            decimal.TryParse(source, out result);
            return result;
        }

        /// <summary>
        /// 字符串转化为Long
        /// </summary>
        public static T ToEnum<T>(this string source) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), source, true);
        }

        /// <summary>
        /// TrimEnd支持string参数类型
        /// </summary>
        /// <param name="input"></param>
        /// <param name="suffixToRemove"></param>
        /// <param name="comparisonType">OrdinalIgnoreCase</param>
        /// <returns></returns>
        public static string TrimEnd(this string input, string suffixToRemove,
    StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            if (input != null && suffixToRemove != null
              && input.EndsWith(suffixToRemove, comparisonType))
            {
                return input.Substring(0, input.Length - suffixToRemove.Length);
            }
            else return input;
        }
        /// <summary>
        /// TrimStart支持string参数类型
        /// </summary>
        /// <param name="input"></param>
        /// <param name="suffixToRemove"></param>
        /// <param name="comparisonType">OrdinalIgnoreCase</param>
        /// <returns></returns>
        public static string TrimStart(this string input, string suffixToRemove,
    StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            if (input != null && suffixToRemove != null
              && input.StartsWith(suffixToRemove, comparisonType))
            {
                return input.Substring(suffixToRemove.Length);
            }
            else return input;
        }

        /// <summary>
        /// 除法(解决除0报错)
        /// </summary>
        public static int Division(int i1, int i2, int percent = 1)
        {
            return i2 == 0 ? 0 : (int)(percent * ((float)i1 / (float)i2));
        }
        /// <summary>
        /// 环比
        /// </summary>
        /// <param name="current">本期</param>
        /// <param name="last">上期</param>
        /// <returns></returns>
        public static int Chain(int current, int last)
        {
            return UtilityExtentions.Division(current - last, last, 100);
        }
    }
}
