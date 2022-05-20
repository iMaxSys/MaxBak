
using iMaxSys.Data.Repositories;
using iMaxSys.Data.EFCore.Repositories;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;

namespace iMaxSys.Identity.Data.Repositories
{
	public interface ISomeRepository : IRepositoryBase
	{
		string Get();
	}

	public class SomeRepository : ISomeRepository
	{
        public void ChangeTable(string table)
        {
            throw new NotImplementedException();
        }

        public string Get()
        {
            return $"{ToString()}";
        }
    }


	public interface IBabyRepository : IRepository<CheckCode>
	{
		string Get();
	}

	public class BabyRepository : EfRepository<CheckCode>, IBabyRepository
	{
        public BabyRepository(IdentityContext context) : base(context)
        {
        }

        public string Get()
		{
			return $"{ToString()}";
		}
	}
}