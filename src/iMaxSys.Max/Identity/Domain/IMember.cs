//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMember.cs
//摘要: IMember
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System;

using iMaxSys.Max.Domain;

namespace iMaxSys.Max.Identity.Domain
{
    /// <summary>
    /// IMember
    /// 唯一标识: <see cref="Id"/>
    /// </summary>
    public interface IMember
    {
        /// <summary>
        /// Id
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// TenantId
        /// </summary>
        long TenantId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// 性别(0男,1女,2未知)
        /// </summary>
        Gender Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        DateTime? Birthday { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        string? LoginName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        string? NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        string? Mobile { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        string? Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        string? Avatar { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        int Type { get; set; }

        /// <summary>
        /// 访问失败次数
        /// </summary>
        int FailedCount { get; set; }

        /// <summary>
        /// RoleId
        /// </summary>
        long RoleId { get; set; }

        /// <summary>
        /// DepartmentId
        /// </summary>
        long DepartmentId { get; set; }

        /// <summary>
        /// 应用来源
        /// </summary>
        AppSource AppSource { get; set; }

        /// <summary>
        /// 账号来源
        /// </summary>
        PlatformSource AccountSource { get; set; }

        /// <summary>
        /// 启用时间
        /// </summary>
        DateTime Start { get; set; }

        /// <summary>
        /// 停用时间
        /// </summary>
        DateTime? End { get; set; }

        /// <summary>
        /// 加入/激活时间
        /// </summary>
        DateTime JoinTime { get; set; }

        /// <summary>
        /// 加入Ip
        /// </summary>
        string? JoinIp { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        DateTime? LastLogin { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        string? LastIp { get; set; }

        /// <summary>
        /// 是否正式
        /// </summary>
        bool IsOfficial { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        long UserId { get; set; }

        /// <summary>
        /// 用户json string
        /// </summary>
        string? UserJson { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        object? User { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        Status Status { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        //IRole Role { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        //IDepartment Department { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        //ITenant Tenant { get; set; }
    }

    /// <summary>
    /// Member<T>
    /// </summary>
    public interface IMember<T> : IMember
    {
        /// <summary>
        /// 更多信息
        /// </summary>
        T Info { get; set; }
    }
}