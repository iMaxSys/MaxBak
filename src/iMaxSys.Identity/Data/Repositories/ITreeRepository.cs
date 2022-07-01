
using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;

namespace iMaxSys.Identity.Data.Repositories;

public interface ITreeRepository<T> : IRepository<T>  where T : Entity
{
 
}

