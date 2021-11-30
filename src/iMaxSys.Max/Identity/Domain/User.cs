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
    /// 用户
    /// </summary>
    public class User : IUser
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
        /// 速查码
        /// </summary>
        public string? QuickCode { get; set; }

        /// <summary>
        /// 性别(0男,1女,2未知)
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

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
        /// 启用时间
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// 停用时间
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// 是否正式
        /// </summary>
        public bool IsOfficial { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }
    }
}
