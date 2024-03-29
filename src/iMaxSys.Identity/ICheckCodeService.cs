﻿//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ICheckCodeService.cs
//摘要: 验证码服务接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity.Models;

namespace iMaxSys.Identity;

/// <summary>
/// 验证码服务接口
/// </summary>
public interface ICheckCodeService : IDependency
{
    /// <summary>
    /// 验证码生成处理
    /// </summary>
    public CheckCodeCreatedHandler CheckCodeCreatedHandler { get; set; }

    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="tenantId"></param>
    /// <param name="bizId"></param>
    /// <param name="bizName"></param>
    /// <param name="memberId"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    Task<CheckCodeResult> MakeAsync(long sid, long tenantId, long bizId, string bizName, long memberId, string to);

    /// <summary>
    /// 检查验证码
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="bizId"></param>
    /// <param name="to"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    Task CheckAsync(long sid, long bizId, string? to, string? code);
}