//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: PhoneNumber.cs
//摘要: 电话号码基类
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

namespace iMaxSys.Sns.Common.Open;

/// <summary>
/// 社交平台电话号码
/// </summary>
public class SnsPhoneNumber
{
    /// <summary>
    /// 用户绑定的手机号（国外手机号会有区号）
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 没有区号的手机号
    /// </summary>
    public string PurePhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 区号
    /// </summary>
    public string CountryCode { get; set; } = string.Empty;
}