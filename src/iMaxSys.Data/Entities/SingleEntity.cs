//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: SingleEntity.cs
//摘要: 单一实体基类
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

namespace iMaxSys.Data.Entities;
/// <summary>
/// 单一实体基类
/// </summary>
public abstract class SingleEntity : Entity
{
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; } = DateTime.Now;
}