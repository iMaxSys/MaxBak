//----------------------------------------------------------------
//Copyright (C) 2016-2048 Co.,Ltd.
//All rights reserved.
//
//文件: Configuration.cs
//摘要: 配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-11-16
//----------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kylin.Data.Models.Auth;
using Kylin.Data.EFCore.EntityConfigurations;
using Kylin.Data.EFCore.EntityConfigurations.Entities;

namespace Kylin.Data.EFCore.EntityConfigurations.Auth;

/// <summary>
/// 客户映射配置
/// </summary>
public class CustomerConfiguration : KylinMasterEntityConfiguration<Customer>
{
    protected override void Configures(EntityTypeBuilder<Customer> builder)
    {
        //基类配置
        base.Configures(builder);

        //消费次数
        builder.Property(x => x.Count).HasColumnName("count").HasComment("消费次数");
        //消费总额
        builder.Property(x => x.Amount).HasColumnName("amount").HasColumnType("decimal(10, 2)").HasComment("消费总额");
        //入场次数
        builder.Property(x => x.InCount).HasColumnName("in_count").HasComment("入场次数");

        //ToTable客户
        builder.ToTable("customer");
    }
}
