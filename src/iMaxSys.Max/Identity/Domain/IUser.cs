//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: RealMember.cs
//摘要: 真实会员
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-16
//----------------------------------------------------------------

using System;

using iMaxSys.Max.Domain;

namespace iMaxSys.Max.Identity.Domain
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public interface IUser
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
        /// 速查码
        /// </summary>
        string? QuickCode { get; set; }

        /// <summary>
        /// 性别(0男,1女,2未知)
        /// </summary>
        Gender Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        DateTime? Birthday { get; set; }

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
        /// 启用时间
        /// </summary>
        DateTime? Start { get; set; }

        /// <summary>
        /// 停用时间
        /// </summary>
        DateTime? End { get; set; }

        /// <summary>
        /// 是否正式
        /// </summary>
        bool IsOfficial { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        Status Status { get; set; }
    }
}
