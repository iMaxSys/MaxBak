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

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories;
using iMaxSys.Identity.Data.Repositories;
using iMaxSys.Data.EFCore.Repositories;
using iMaxSys.Data.Entities;
using iMaxSys.Identity.Data.EFCore;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 身份通用仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public class IdentityRepository<T> : EfRepository<T>, IIdentityRepository<T> where T : Entity
{
    public IdentityRepository(IdentityContext context) : base(context)
    {
    }
}

public class IdentityReadOnlyRepository<T> : EfReadOnlyRepository<T>, IIdentityReadOnlyRepository<T> where T : Entity
{
    public IdentityReadOnlyRepository(IdentityReadOnlyContext context) : base(context)
    {
    }
}

/*
public class IdentityRepository : IIdentityRepository
{
    protected const string TAG = "id:";
    protected const string TAG_ACCESS = "a:";
    protected const string TAG_MEMBER = "u:";
    protected const string TAG_TENANT = "t:";
    protected const string TAG_ROLE = "r:";
    protected const string TAG_MENU = "m:";

    protected const string TAG_TENANT_MENU = $"{TAG}{TAG_MENU}";
    protected const string TAG_TENANT_ROLE = $"{TAG}{TAG_ROLE}";

    private readonly IIdentityCache _identityCache;

    public IdentityRepository(IIdentityCache identityCache)
    {
        _identityCache = identityCache;
    }

    /// <summary>
    /// 获取访问Session
    /// </summary>
    /// <param name="token">令牌</param>
    /// <returns></returns>
    public async Task<IAccessSession?> GetAccessSessionAsync(string token)
    {
        return await _identityCache.GetAsync<AccessSession>($"{TAG}{TAG_ACCESS}{token}", true);
    }

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="memberId">成员id</param>
    /// <returns></returns>
    public async Task<IMember?> GetMemberAsync(string memberId)
    {
        return await _identityCache.GetAsync<Max.Identity.Domain.Member>($"{TAG}{TAG_MEMBER}{memberId}", true);
    }

    public Task<IMember?> ReadAsync(string memberId)
    {
        throw new NotImplementedException();
    }
}
*/