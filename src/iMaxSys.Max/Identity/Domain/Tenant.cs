//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ITenant.cs
//摘要: ITenant
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-01-01
//----------------------------------------------------------------

using System;

using iMaxSys.Max.Domain;

namespace iMaxSys.Max.Identity.Domain
{
    public class Tenant : ITenant
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Start
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// End
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public Status Status { get; set; }
    }

    public class Tenant<T> : Tenant, ITenant<T>
    {
        public T Info { get; set; }
    }
}
