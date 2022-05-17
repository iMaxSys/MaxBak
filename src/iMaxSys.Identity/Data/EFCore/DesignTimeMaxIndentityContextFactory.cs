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


public class DesignTimeMaxIdentityContextFactory : IDesignTimeDbContextFactory<MaxIdentityContext>
{
    public MaxIdentityContext CreateDbContext(string[] args)
    {
        return new MaxIdentityContext(new DesignTimeMaxOptions());
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
            new DatabaseOption { Type = 0, Connection = "server=localhost;port=8806;database=max;uid=muser;pwd=yaoniming$3000A;Charset=utf8mb4;" }
        }
        }
    };
}
