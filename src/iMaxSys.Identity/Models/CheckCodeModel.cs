//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CheckCodeModel.cs
//摘要: 验证码模型
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-16
//----------------------------------------------------------------

namespace iMaxSys.Identity.Models;

/// <summary>
/// 验证码模型
/// </summary>
public class CheckCodeModel
{
    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; set; } = String.Empty;

    /// <summary>
    /// Expires
    /// </summary>
    public DateTime Expires { get; set; }

    /// <summary>
    /// BizName
    /// </summary>
    public string BizName { get; set; } = String.Empty;
}