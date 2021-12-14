//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: TenantMasterEntity.cs
//摘要: TenantMasterEntity
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

namespace iMaxSys.Data.Entities;

public abstract class TenantMasterEntity : MasterEntity
{
    /// <summary>
    /// TenantId
    /// </summary>
    public long TenantId { get; set; }
}