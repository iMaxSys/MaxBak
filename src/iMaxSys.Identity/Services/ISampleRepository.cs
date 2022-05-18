
using System;
using iMaxSys.Data;
using iMaxSys.Data.Entities;
using iMaxSys.Max.DependencyInjection;

using iMaxSys.Data.Entities.App;
using iMaxSys.Data.Repositories;
using iMaxSys.Data.EFCore.Repositories;
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

    public interface ISampleRepository : IRepository<XppSns>
    {
    }

    public class SampleRepository : EfRepository<XppSns>, ISampleRepository
    {
        public SampleRepository(MaxIdentityContext context) : base(context)
        {
        }
    }
}