
using System;
using iMaxSys.Max.Data.Entities;
using iMaxSys.Max.Domain;

namespace iMaxSys.Identity.Data.Models
{
    public class MemberSession : TenantMasterEntity
    {
        /// <summary>
        /// 应用x社交账号Id
        /// </summary>
        public long XppSnsId { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        public long MemberId { get; set; }

        /// <summary>
        /// Token For MaxOne
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// 第三方平台统一Id
        /// </summary>
        public string UnionId { get; set; } = string.Empty;

        /// <summary>
        /// 第三方平台Id
        /// </summary>
        public string OpenId { get; set; } = string.Empty;

        /// <summary>
        /// SessionKey:目前微信的session_key有效期是三天
        /// </summary>
        public string? SessionKey { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; } = string.Empty;

        /// <summary>
        /// Token过期时间
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// 是否正式成员
        /// </summary>
        public bool IsOfficial { get; set; } = false;

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; } = Status.Enable;
    }
}