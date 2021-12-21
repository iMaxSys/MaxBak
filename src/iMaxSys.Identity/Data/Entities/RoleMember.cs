//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: RoleMember.cs
//摘要: RoleMember 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Domain;
using iMaxSys.Data.Entities;

namespace iMaxSys.Identity.Data.Entities;

/// <summary>
/// RoleMember
/// </summary>
public class RoleMember : TenantMasterEntity
{
    /// <summary>
    /// MemberId
    /// </summary>
    public long MemberId { get; set; }

    /// <summary>
    /// RoleId
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// MemberId
    /// </summary>
    public Status Status { get; set; } = Status.Enable;

    /// <summary>
    /// Role
    /// </summary>
    public Role Role { get; set; } = new();

    /// <summary>
    /// Member
    /// </summary>
    public Member Member { get; set; } = new();
}