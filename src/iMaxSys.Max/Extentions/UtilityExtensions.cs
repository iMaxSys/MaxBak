//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: UtilityExtention.cs
//摘要: UtilityExtention
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Extentions;

/// <summary>
/// 工具扩展
/// </summary>
public static class UtilityExtensions
{
    //常量定义
    const string FORMAT_DATE = "yyyy-MM-dd";
    const string FORMAT_CHNDATE = "yyyy年MM月dd日";
    const string FORMAT_TIME = "yyyy-MM-dd HH:mm:ss";
    const string FORMAT_SHORTTIME = "yy-MM-dd HH:mm:ss";
    const string FORMAT_LONGTIME = "yyyy-MM-dd HH:mm:ss.fff";
    const string FORMAT_DATESHORTTIME = "yyyy-MM-dd HH:mm";
    const string FORMAT_ONLYTIME = "HH:mm";
    const string FORMAT_MONTH = "yyyy-MM";
    const string SEPARATER = ",";

    static readonly DateTime StartTimePoint;

    static UtilityExtensions()
    {
        StartTimePoint = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
    }

    /// <summary>
    /// 值类型非空执行
    /// </summary>
    /// <param name="source"></param>
    /// <param name="action"></param>
    /// <param name="actionIfNull"></param>
    public static void IfNotNull<T>(this T source, Action<T> action) where T : struct
    {
        action(source);
    }

    /// <summary>
    /// 值类型非空执行
    /// </summary>
    /// <param name="source"></param>
    /// <param name="action"></param>
    /// <param name="actionIfNull"></param>
    public static void IfNotNull<T>(this T? source, Action<T?> action) where T : struct
    {
        action(source);
    }

    /// <summary>
    /// 字符串非空执行
    /// </summary>
    /// <param name="source"></param>
    /// <param name="action"></param>
    /// <param name="actionIfNull"></param>
    public static void IfNotNull(this string source, Action<string> action, Action? actionIfNull = null)
    {
        if (!string.IsNullOrWhiteSpace(source))
        {
            action(source);
        }
        else
        {
            actionIfNull?.Invoke();
        }
    }

    /// <summary>
    /// 字符串非空执行
    /// </summary>
    /// <param name="source"></param>
    /// <param name="action"></param>
    /// <param name="actionIfNull"></param>
    public static string? IfNotNull(this string source, string? value)
    {
        if (!string.IsNullOrWhiteSpace(source))
        {
            return source;
        }
        else
        {
            return value;
        }
    }

    /// <summary>
    /// 引用类型非空执行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="action"></param>
    /// <param name="actionIfNull"></param>
    public static void IfNotNull<T>(this T source, Action<T> action, Action? actionIfNull = null) where T : class
    {
        if (source != null)
        {
            action(source);
        }
        else
        {
            actionIfNull?.Invoke();
        }
    }

    /// <summary>
    /// 非空执行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="source"></param>
    /// <param name="func"></param>
    /// <param name="ifNull"></param>
    /// <returns></returns>
    public static TResult IfNotNull<T, TResult>(this T source, Func<T, TResult> func, Func<TResult>? ifNull = null) where T : class where TResult : new()
    {
        return source != null ? func(source) : (ifNull != null ? ifNull() : new TResult());
    }

    /// <summary>
    /// 整型位开关(11001->3=true)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="index">位数(from 0)</param>
    /// <returns></returns>
    public static bool IndexEnable(this int value, int index) => (value / (int)Math.Pow(10, index)) % 10 == 1;

    /// <summary>
    /// 整型位开关(11001->3=true)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="index">位数(from 0)</param>
    /// <returns></returns>
    public static bool IndexEnable(this int? value, int index) => value == null || (value.Value / (int)Math.Pow(10, index)) % 10 == 1;

    /// <summary>
    /// 位值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="index">位数(from 0)</param>
    /// <returns></returns>
    public static int IndexValue(this long value, int index) => (int)((value / (int)Math.Pow(10, index)) % 10);

    /// <summary>
    /// 转换为整数金额
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToInt(this decimal value) => value.ToString("f0");

    /// <summary>
    /// 转换为两位小数金额
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToMoneyString(this decimal value) => value.ToString("f2");

    /// <summary>
    /// 转换为折扣率
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToDiscountString(this decimal value) => value.ToString("f1");

    /// <summary>
    /// 转换为整数金额
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string? ToIntMoneyString(this decimal? value) => value?.ToString("f0");

    /// <summary>
    /// 转换为两位小数金额
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string? ToMoneyString(this decimal? value) => value?.ToString("f2");

    /// <summary>
    /// 格式化普通格式日期字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToDateString(this DateTime dateTime) => dateTime.ToString(FORMAT_DATE);

    /// <summary>
    /// 格式化中文日期字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToChnDateString(this DateTime dateTime) => dateTime.ToString(FORMAT_CHNDATE);

    /// <summary>
    /// 格式化普通格式时间字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToNormalString(this DateTime dateTime) => dateTime.ToString(FORMAT_TIME);

    /// <summary>
    /// 格式化为时间字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string? ToNormalString(this DateTime? dateTime) => dateTime?.ToString(FORMAT_TIME);

    /// <summary>
    /// 格式化长格式时间字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToLongString(this DateTime dateTime) => dateTime.ToString(FORMAT_LONGTIME);

    /// <summary>
    /// 格式化短格式时间字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToShortString(this DateTime dateTime) => dateTime.ToString(FORMAT_SHORTTIME);

    /// <summary>
    /// 格式化为日期时分时间字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToDateShortTimeString(this DateTime dateTime) => dateTime.ToString(FORMAT_DATESHORTTIME);

    // <summary>
    /// 格式化为日期时分时间字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string? ToDateShortTimeString(this DateTime? dateTime) => dateTime?.ToString(FORMAT_DATESHORTTIME);

    /// <summary>
    /// 格式化时间字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToTimeString(this DateTime dateTime) => dateTime.ToString(FORMAT_ONLYTIME);

    /// <summary>
    /// 格式化年月时间字符串
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToMonthString(this DateTime dateTime) => dateTime.ToString(FORMAT_MONTH);

    /// <summary>
    /// 获取当前时间戳
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ToTimestamp(this DateTime dateTime) => (long)((dateTime - StartTimePoint).TotalSeconds);

    /// <summary>
    /// 获取当前时间戳
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ToLongTimestamp(this DateTime dateTime) => (long)((dateTime - StartTimePoint).TotalMilliseconds);

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
    public static DateTime ToDateTime(this long number) => StartTimePoint.AddMilliseconds(number).ToLocalTime();

    /// <summary>
    /// 获取时间根据字符串
    /// </summary>
    public static DateTime StrToDatetime(this string dateTime) => DateTime.Parse(dateTime);

    /// <summary>
    /// 获取当前周
    /// </summary>
    public static string ToWeek(this DateTime dateTime) => System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);

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
    public static DateTime ToMonthFirstDay(this DateTime dateTime) => dateTime.AddDays(1 - dateTime.Day).Date;

    /// <summary>
    /// 获取本月最后一天
    /// </summary>
    public static DateTime ToMonthLastDay(this DateTime dateTime) => dateTime.ToMonthFirstDay().AddMonths(1).AddSeconds(-1);

    /// <summary>
    /// 获取本年第一天
    /// </summary>
    public static DateTime ToYearFirstDay(this DateTime dateTime) => dateTime.AddMonths(1 - dateTime.Month).AddDays(1 - dateTime.Day).Date;

    /// <summary>
    /// 获取本月最后一天
    /// </summary>
    public static DateTime ToYearLastDay(this DateTime dateTime) => dateTime.ToYearFirstDay().AddYears(1).AddSeconds(-1);

    /// <summary>
    /// 获取字符串左起指定长度字符串
    /// </summary>
    /// <param name="source">字符串</param>
    /// <param name="length">长度</param>
    /// <returns></returns>
    public static string Left(this string source, int length) => source[..Math.Min(source.Length, length)];

    /// <summary>
    /// 获取字符串右起指定长度字符串
    /// </summary>
    /// <param name="source">字符串</param>
    /// <param name="length">长度</param>
    /// <returns></returns>
    public static string Right(this string source, int length) => source[^length..];

    /// <summary>
    /// 转为int数组
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int[] ToIntArray(this string source) => Array.ConvertAll<string, int>(source.Split(SEPARATER), int.Parse);

    /// <summary>
    /// 转为long数组
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static long[] ToLongArray(this string source) => Array.ConvertAll<string, long>(source.Split(SEPARATER), long.Parse);

    /// <summary>
    /// 转为字符串数组
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string[] ToStringArray(this string source) => source.Split(SEPARATER);

    /// <summary>
    /// 转为字典(from->k1:v1,k2:v2)
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Dictionary<string, string>? ToStringDictionary(this string source) => string.IsNullOrWhiteSpace(source) ? null : source.Split(SEPARATER).Select(x => x.Split(":")).ToDictionary(x => x[0], x => x.Length > 1 ? x[1] : x[0]);


    #region Separater String Left Right

    private static string GetSeparaterLeft(string source, string separater) => source.IndexOf(separater) > 0 ? source[..source.IndexOf(separater)] : source;

    private static string GetSeparaterRight(string source, string separater)
    {
        return source.IndexOf(separater) > 0 ? source.Substring(source.IndexOf(separater) + separater.Length, source.Length - source.IndexOf(separater) - separater.Length) : source;
    }

    /// <summary>
    /// 获取分隔符左边的字符串
    /// </summary>
    /// <param name="source">字符串</param>
    /// <returns></returns>
    public static string Left(this string source) => GetSeparaterLeft(source, SEPARATER);

    /// <summary>
    /// 获取分隔符左边的字符串
    /// </summary>
    /// <param name="source">字符串</param>
    /// <param name="separater">分隔符</param>
    /// <returns></returns>
    public static string Left(this string source, string separater) => GetSeparaterLeft(source, separater);

    /// <summary>
    /// 获取分隔符右边的字符串
    /// </summary>
    /// <param name="source">字符串</param>
    /// <returns></returns>
    public static string Right(this string source) => GetSeparaterRight(source, SEPARATER);

    /// <summary>
    /// 获取分隔符右边的字符串
    /// </summary>
    /// <param name="source">字符串</param>
    /// <param name="separater">分隔符</param>
    /// <returns></returns>
    public static string Right(this string source, string separater) => GetSeparaterRight(source, separater);

    #endregion

    /// <summary>
    /// Base64编码
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string EncodeBase64(this string source)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(source);
        string encode;
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
        byte[] bytes = Convert.FromBase64String(source);
        string decode;
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
    public static string? ToNullable(this string source) => string.IsNullOrWhiteSpace(source) ? null : source;

    /// <summary>
    /// 指示指定的 <see cref="System.String"/> 对象是 null 还是 System.String.Empty 字符串。
    /// </summary>
    public static bool IsNullOrEmpty(this String data) => String.IsNullOrEmpty(data);

    /// <summary>
    /// 指示指定的字符串是 null、空还是仅由空白字符组成。
    /// </summary>
    public static bool IsNullOrWhiteSpace(this String data) => String.IsNullOrWhiteSpace(data);

    /// <summary>
    /// 如果指定的 <see cref="System.String"/> 对象字符串是 null、空还是仅由空白字符组成则返回默认值。
    /// </summary>
    public static string IfNullOrWhiteSpace(this string value, string defaultValue) => (!value.IsNullOrWhiteSpace() ? value : defaultValue);

    /// <summary>
    /// 如果指定的 <see cref="System.String"/> 对象是 null 或 System.Empty 字符串则返回默认值。
    /// </summary>
    public static string IfNullOrEmpty(this string value, string defaultValue) => (!value.IsNullOrEmpty() ? value : defaultValue);

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
    public static string GetFileNameExtension(this string value) => value[(value.LastIndexOf(".") + 1)..];

    /// <summary>
    /// 获取应用相关程序集(为提高性能,只加载框架相关程序集和应用程序程序集)
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetAppAssemblies()
    {
        string appName = AppDomain.CurrentDomain.FriendlyName.Left(".");
        string fxName = Assembly.GetExecutingAssembly().FullName!.Left(".");
        return DependencyContext.Default.CompileLibraries.Where(c => c.Name.StartsWith(appName, StringComparison.CurrentCultureIgnoreCase) || c.Name.StartsWith(fxName, StringComparison.CurrentCultureIgnoreCase)).Select(x => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(x.Name)));
    }

    /// <summary>
    /// 获取应用相关实现类型
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Type> GetAppTypes() => GetAppAssemblies().SelectMany(x => x.GetTypes()).Where(c => c.IsClass && !c.IsAbstract);

    /// <summary>
    /// 字符串转化为Int
    /// </summary>
    public static int? ToInt(this string source) => int.TryParse(source, out int result) ? result : null;

    /// <summary>
    /// 字符串转化为Long
    /// </summary>
    /// <param name="source">字符串</param>
    /// <returns></returns>
    public static long? ToLong(this string source) => long.TryParse(source, out long result) ? result : null;

    /// <summary>
    /// 字符串转化为Decimal
    /// </summary>
    /// <param name="source">字符串</param>
    /// <returns></returns>
    public static decimal? ToDecimal(this string source) => decimal.TryParse(source, out decimal result) ? result : null;

    /// <summary>
    /// 字符串转化为Long
    /// </summary>
    public static T ToEnum<T>(this string source) where T : Enum => (T)Enum.Parse(typeof(T), source, true);

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
        return UtilityExtensions.Division(current - last, last, 100);
    }
}
