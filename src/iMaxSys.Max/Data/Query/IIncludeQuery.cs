
using System;
using System.Collections.Generic;

namespace iMaxSys.Max.Data.Query
{
    /// <summary>
    /// IIncludeQuery
    /// </summary>
    public interface IIncludeQuery
    {
        Dictionary<IIncludeQuery, string> PathMap { get; }
        IncludeVisitor Visitor { get; }
        HashSet<string> Paths { get; }
    }

    public interface IIncludeQuery<TEntity, out TPreviousProperty> : IIncludeQuery
    {
    }
}