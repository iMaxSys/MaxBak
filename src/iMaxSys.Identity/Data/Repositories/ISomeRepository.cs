using System;
using iMaxSys.Data;

namespace iMaxSys.Identity.Data.Repositories
{
	public interface ISomeRepository : ICustomRepository
	{
		string Get();
	}

	public class SomeRepository : ISomeRepository
	{
        public string Get()
        {
            return $"{ToString()}";
        }
    }
}