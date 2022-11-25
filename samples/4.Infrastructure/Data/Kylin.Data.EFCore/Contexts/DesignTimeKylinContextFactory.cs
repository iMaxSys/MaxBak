using System;
using iMaxSys.Max.Options;
using Kylin.Framework.Options;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Kylin.Data.EFCore.Contexts;


public class DesignTimeKylinContextFactory : IDesignTimeDbContextFactory<KylinContext>
{
    public KylinContext CreateDbContext(string[] args)
    {
        return new KylinContext(new DesignTimeKylinOptions(), new DesignTimeMaxOptions());
    }
}

public class DesignTimeKylinOptions : IOptions<KylinOption>
{
    //开发
    public KylinOption Value => new()
    {
        Databases = new List<DatabaseOption>
            {
                new DatabaseOption { Type = 0, Connection = "server=127.0.0.1;port=3306;database=kylin;uid=root;pwd=123456;Charset=utf8mb4;" }
                //new DatabaseOption { Type = 1, Connection = "Server=127.0.0.1;Database=max;User Id=sa;Password=iam@Apassword" }
            }
    };
}

public class DesignTimeMaxOptions : IOptions<MaxOption>
{
    public MaxOption Value => new()
    {
        Identity = new IdentityOption
        {
            Databases = new List<DatabaseOption>
            {
                new DatabaseOption { Type = 0, Connection = "server=127.0.0.1;port=3306;database=max;uid=root;pwd=123456;Charset=utf8mb4;" }
                //new DatabaseOption { Type = 1, Connection = "Server=127.0.0.1;Database=max;User Id=sa;Password=iam@Apassword" }
            }
        }
    };
}