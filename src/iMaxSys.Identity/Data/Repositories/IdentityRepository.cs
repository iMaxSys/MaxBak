//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IIDentityRepository.cs
//摘要: 身份仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Caching;
using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories;
using iMaxSys.Data.EFCore.Repositories;
using iMaxSys.Data.Entities;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Repositories;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 身份通用仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public class IdentityRepository<T> : EfRepository<T>, IIdentityRepository<T> where T : Entity
{
    //全局缓存标志
    protected const bool _global = true;

    protected const string TAG = "i";
    protected const string TAG_ACCESS = "a";
    protected const string TAG_MEMBER = "m";
    protected const string TAG_USER = "u";
    protected const string TAG_TENANT = "t";
    protected const string TAG_ROLE = "r";
    protected const string TAG_MENU = "n";

    protected readonly string _tagId = string.Empty;
    protected readonly string _tagAccess = string.Empty;
    protected readonly string _tagMember = string.Empty;
    protected readonly string _tagUser = string.Empty;
    protected readonly string _tagRole = string.Empty;
    protected readonly string _tagMenu = string.Empty;

    protected readonly IMapper Mapper;
    protected readonly ICache Cache;
    protected readonly MaxOption Option;

    public IdentityRepository(IdentityContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context)
    {
        Mapper = mapper;
        Option = option.Value;
        Cache = cacheFactory.GetService();

        _tagId = $"{TAG}{Cache.Separator}";
        _tagAccess = $"{_tagId}{TAG_ACCESS}{Cache.Separator}";
        _tagMember = $"{_tagId}{TAG_MEMBER}{Cache.Separator}";
        _tagUser = $"{_tagId}{TAG_USER}{Cache.Separator}";
        _tagRole = $"{_tagId}{TAG_ROLE}{Cache.Separator}";
        _tagMenu = $"{_tagId}{TAG_MENU}{Cache.Separator}";
    }
}

/// <summary>
/// 身份只读仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public class IdentityReadOnlyRepository<T> : EfReadOnlyRepository<T>, IIdentityReadOnlyRepository<T> where T : Entity
{
    public IdentityReadOnlyRepository(IdentityReadOnlyContext context) : base(context)
    {
    }
}