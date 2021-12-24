//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CheckCodeRepository.cs
//摘要: 验证码仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Data.Repositories.EFCore;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 验证码仓储
/// </summary>
public class CheckCodeRepository : EfRepository<CheckCode>, ICheckCodeRepository
{
    public CheckCodeRepository(MaxIdentityContext context) : base(context)
    {
    }
}