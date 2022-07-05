//----------------------------------------------------------------
//Copyright (C) 2016-2026 Care Co.,Ltd.
//All rights reserved.
//
//文件: DesignTimeMaxIdentityContextFactory.cs
//摘要: 身份系统上下文工厂 
//说明: 用于数据迁移
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Options;

namespace iMaxSys.Identity.Data.EFCore;


public class DesignTimeMaxIdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
    public IdentityContext CreateDbContext(string[] args)
    {
        return new IdentityContext(new DesignTimeMaxOptions());
    }
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