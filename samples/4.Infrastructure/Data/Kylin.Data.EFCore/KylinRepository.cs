//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: KylinRepository.cs
//摘要: KylinRepository 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using System;

using iMaxSys.Data.Entities;
using iMaxSys.Data.EFCore.Repositories;

using Kylin.Data.EFCore.Contexts;

namespace Kylin.Data.EFCore;

public class KylinRepository<T> : EfRepository<T>, IKylinRepository<T> where T : Entity
{
    public KylinRepository(KylinContext context) : base(context)
    {
    }
}

