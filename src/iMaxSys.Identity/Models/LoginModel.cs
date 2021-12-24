//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: LoginResult.cs
//摘要: 登录结果
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-05-01
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 登录结果
/// </summary>
public class LoginModel
{
    /// <summary>
    /// 访问令牌
    /// </summary>
    public IAccessToken? AccessToken { get; set; }

    /// <summary>
    /// 成员
    /// </summary>
    public IMember? Member { get; set; }
}
