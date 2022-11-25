//----------------------------------------------------------------
//Copyright (C) 2016-2025 Co.,Ltd.
//All rights reserved.
//
//文件: KylinContext.cs
//摘要: KylinContext 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;


using iMaxSys.Data.EFCore;
using iMaxSys.Max.Options;
using iMaxSys.Max.Environment.Access;

using Kylin.Framework.Options;
using Kylin.Data.Models.Entities;

namespace Kylin.Data.EFCore.Contexts;

/// <summary>
/// MaxContext
/// </summary>
public class KylinContext : EfDbContext
{
    public KylinContext(IOptions<KylinOption> kylinOption, IOptions<MaxOption> maxOption) : base(kylinOption.Value.Databases ?? maxOption.Value.Core.Databases)
    {
    }
}

public class KylinReadOnlyContext : EfReadOnlyDbContext
{
    public KylinReadOnlyContext(IOptions<KylinOption> kylinOption, IOptions<MaxOption> maxOption) : base(kylinOption.Value.Databases ?? maxOption.Value.Core.Databases)
    {
    }
}