//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: EntityConfiguration.cs
//摘要: 实体配置抽象类 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max;
using iMaxSys.Data.Entities;

namespace iMaxSys.Data.EFCore.Repositories.Configurations;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    /// <summary>
    /// 自动Id生成
    /// </summary>
    protected virtual bool AutoId { get; } = true;

    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedNever();
        if (AutoId)
        {
            builder.Property(x => x.Id).Metadata.SetValueGeneratorFactory((p, t) => Id);
        }
        builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.HasQueryFilter(x => !x.IsDeleted);

        Configures(builder);
        //builder.ToTable(ToUnderscoreLower(typeof(T).Name));
    }

    /// <summary>
    /// 定制配置
    /// </summary>
    protected abstract void Configures(EntityTypeBuilder<T> builder);

    /// <summary>
    /// 转换为下划线小写格式
    /// </summary>
    /// <param name="source">source</param>
    /// <returns>result</returns>
    protected static string ToUnderscoreLower(string source)
    {
        return Regex.Replace(source, @"((?<=.)[A-Z][a-z]*)|((?<=[a-zA-Z])\d+)", @"_$1$2").ToLower();
    }

    /// <summary>
    /// Id
    /// </summary>
    public virtual ValueGenerator Id => IdGenerator.Value;
}

public static class IdGenerator
{
    public static ValueGenerator Value { get; set; }

    static IdGenerator()
    {
        Value = new IdValueGenerator();
    }
}

/// <summary>
/// 主键生成器
/// </summary>
public class IdValueGenerator : ValueGenerator
{
    public override bool GeneratesTemporaryValues => false;

    protected override object NextValue(EntityEntry entry) => IdWorker.NextId();
}