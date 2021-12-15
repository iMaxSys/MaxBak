//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ICheckCodeRepository.cs
//摘要: 验证码仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Data;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity.Data.Models;
using iMaxSys.Identity.Models;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 验证码仓储接口
/// </summary>
public interface ICheckCodeRepository : IDependency, ICustomRepository, IRepository<CheckCode>
{
}


