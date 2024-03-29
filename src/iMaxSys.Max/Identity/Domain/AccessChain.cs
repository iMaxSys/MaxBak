﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AccessChain.cs
//摘要: AccessChain
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// AccessChain
/// </summary>
public class AccessChain : IAccessChain
{
    /// <summary>
    /// AccessSession
    /// </summary>
    public IAccessSession AccessSession { get; set; } = new AccessSession();

    /// <summary>
    /// Member
    /// </summary>
    public IMember Member { get; set; } = new Member();

    /// <summary>
    /// User
    /// </summary>
    public IUser? User { get; set; }
}