//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Entity.cs
//摘要: 实体模型基类
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

namespace iMaxSys.Data.Entities;

/// <summary>
/// 实体模型基类
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 软删除标志(0:未删除,1:已删除)
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}
