// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: KylinMasterEntity.cs
//摘要: KylinMasterEntity
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-15
//----------------------------------------------------------------

using iMaxSys.Data.Entities;

namespace Kylin.Data.Models.Entities;

/// <summary>
/// KylinMasterEntity
/// </summary>
public abstract class KylinMasterEntity : MasterEntity
{
    /// <summary>
    /// 集团Id
    /// </summary>
    public long GroupId { get; set; }

    /// <summary>
    /// 公司Id
    /// </summary>
    public long CompanyId { get; set; }

    /// <summary>
    /// 门店/场馆Id
    /// </summary>
    public long StoreId { get; set; }
}