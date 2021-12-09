//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Member.cs
//摘要: Member
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
    /// Member
    /// </summary>
    public class Member : IMember
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// TenantId
        /// </summary>
        public long TenantId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 性别(0男,1女,2未知)
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 登录名/用户名
        /// </summary>
        public string? LoginName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string? NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? Mobile { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 访问失败次数
        /// </summary>
        public int FailedCount { get; set; }

        /// <summary>
        /// RoleId
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// DepartmentId
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 应用来源
        /// </summary>
        public XppSource XppSource { get; set; }

        /// <summary>
        /// 账号来源
        /// </summary>
        public PlatformSource AccountSource { get; set; }

        /// <summary>
        /// 启用时间
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// 停用时间
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// 加入/激活时间
        /// </summary>
        public DateTime JoinTime { get; set; }

        /// <summary>
        /// 加入Ip
        /// </summary>
        public string? JoinIp { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string? LastIp { get; set; }

        /// <summary>
        /// 是否正式
        /// </summary>
        public bool IsOfficial { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户json string
        /// </summary>
        public string? UserJson { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public object? User { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        //public IRole Role { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        //public IDepartment Department { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        //public ITenant Tenant { get; set; }
    }

    public class Member<T> : Member, IMember<T>
    {
        public T? Info { get; set; }
    }
}
