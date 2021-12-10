
using System;
using iMaxSys.Max.Options;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace iMaxSys.Identity.Data.EFCore
{
    public class DesignTimeMaxIdentityContextFactory : IDesignTimeDbContextFactory<MaxIdentityContext>
    {
        public MaxIdentityContext CreateDbContext(string[] args)
        {
            return new MaxIdentityContext(new DesignTimeMaxOptions());
        }
    }

    public class DesignTimeMaxOptions : IOptions<MaxOption>
    {
        //public MaxOption Value => new() { Core = new CoreOption() { Connection = "server=127.0.0.1;port=8806;database=max;uid=muser;pwd=yaoniming$3000A;Charset=utf8mb4;", Type = 0 } };
        public MaxOption Value => new() { Core = new CoreOption() { Connection = "server=localhost;port=3306;database=max;uid=root;pwd=iamapassword;Charset=utf8mb4;", Type = 0 } };
    }
}
