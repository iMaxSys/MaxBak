using System;
using iMaxSys.Max.Data;
using iMaxSys.Max.Data.EFCore;
using iMaxSys.Max.Data.Entities;
using iMaxSys.Max.DependencyInjection;

using iMaxSys.Core.Data.Models;
using iMaxSys.Identity.Data.EFCore;

namespace iMaxSys.Identity.Services
{
    //public interface IGenericRepository<T> :IRepository<T>, IDependency where T : Entity
    //{
    //}

    //public class GenericRepository<T> : EfRepository<T>, IGenericRepository<T> where T : Entity
    //{
    //    public GenericRepository(MaxContext context) : base(context)
    //    {
    //    }
    //}

    //=========================================================================================================

    public interface ISampleRepository : IRepository<XappSns>
    {
    }

    public class SampleRepository : EfRepository<XappSns>, ISampleRepository
    {
        public SampleRepository(MaxIdentityContext context) : base(context)
        {
        }
    }
}
