//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: RoleRepository.cs
//摘要: 角色仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-01-07
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.EFCore;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;
using DbRole = iMaxSys.Identity.Data.Entities.Role;

namespace iMaxSys.Identity.Data.Repositories;

/*
/// <summary>
/// 角色仓储
/// </summary>
public class RoleRepository : EfRepository<DbRole>, IRoleRepository
{
    const string TAG = "id:";
    const string TAG_ROLE = "r:";
    const string TAG_ROLE_SECTION = $"{TAG}{TAG_ROLE}";

    private readonly MaxOption _option;
    private readonly IIdentityCache _identityCache;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityCache"></param>
    public RoleRepository(MaxIdentityContext context, IOptions<MaxOption> option, IIdentityCache identityCache) : base(context)
    {
        _option = option.Value;
        _identityCache = identityCache;
    }

    /// <summary>
    /// 读取角色from cache
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<IRole?> ReadAsync(long roleId)
    {
        return await _identityCache.GetAsync<Max.Identity.Domain.Role>($"{TAG_ROLE_SECTION}{roleId}", true);
    }

    /// <summary>
    /// 刷新角色cache
    /// </summary>
    /// <param name="role"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    public async Task RefreshAsync(IRole role, DateTime? expires = null)
    {
        if (role?.Id > 0)
        {
            await _identityCache.SetAsync($"{TAG_ROLE_SECTION}{role.Id}", role, expires ?? DateTime.Now.AddMinutes(_option.Identity.Refresh), true);
        }
    }

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task RemoveAsync(long roleId)
    {
        //软删除
        this.Remove(roleId);

        //清除缓存
        await _identityCache.DeleteAsync($"{TAG_ROLE_SECTION}{roleId}", true);
    }
}
*/