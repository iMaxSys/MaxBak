//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MaxIdentityRepository.cs
//摘要: 身份通用仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Data;
using iMaxSys.Max.Data.EFCore;
using iMaxSys.Max.Data.Entities;
using iMaxSys.Max.DependencyInjection;

using iMaxSys.Identity.Data.EFCore;

namespace iMaxSys.Identity.Data.Repositories
{
    /// <summary>
    /// 身份通用仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MaxIdentityRepository<T> : EfRepository<T>, IMaxIdentityRepository<T> where T : Entity
    {
        public MaxIdentityRepository(MaxIdentityContext context) : base(context)
        {
        }
    }
}
