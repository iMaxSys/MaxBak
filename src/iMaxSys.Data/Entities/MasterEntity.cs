//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MasterEntity.cs
//摘要: 主实体基类
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

namespace iMaxSys.Data.Entities;

/// <summary>
/// 主实体基类
/// </summary>
public abstract class MasterEntity : SingleEntity
{
    /// <summary>
    /// 创建者标识
    /// </summary>
    public long CreatorId { get; set; } = 0;

    /// <summary>
    /// 创建者名称
    /// </summary>
    public string Creator { get; set; } = "0";

    /// <summary>
    /// 修改者标识
    /// </summary>
    public long ReviserId { get; set; } = 0;

    /// <summary>
    /// 修改者名称
    /// </summary>
    public string Reviser { get; set; } = "0";

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? ReviseTime { get; set; } = DateTime.Now;
}
