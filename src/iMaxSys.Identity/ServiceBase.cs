//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ServiceBase.cs
//摘要: 服务基类
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Data;
using iMaxSys.Identity.Data.EFCore;

namespace iMaxSys.Identity;

/// <summary>
/// 服务基类
/// </summary>
public class ServiceBase
{
    protected readonly IMapper Mapper;
    protected readonly MaxOption Option;
    protected readonly IGenericCache Cache;
    protected readonly IUnitOfWork UnitOfWork;

    public ServiceBase(IMapper mapper, IOptions<MaxOption> option, IGenericCache genericCache, UnitOfWork<IdentityContext> unitOfWork)
    {
        Mapper = mapper;
        Option = option.Value;
        Cache = genericCache;
        UnitOfWork = unitOfWork;
    }
}