//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: RegisterModel.cs
//摘要: 用户注册模型
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Common.Domain;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 用户注册模型
/// </summary>
public class RegisterRequest : DomainRequest
{
    /// <summary>
    /// Sid
    /// </summary>
    public long SID { get; set; }

    /// <summary>
    /// 推荐人Id
    /// </summary>
    public long ReferrerId { get; set; }

    /// <summary>
    /// 注册方式
    /// </summary>
    public int Way { get; set; }

    /// <summary>
    /// 用户类型
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// OpenId
    /// </summary>
    public string? OpenId { get; set; }

    /// <summary>
    /// UnionId
    /// </summary>
    public string? UnionId { get; set; }

    /// <summary>
    /// 社交平台code
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    public string? CheckCode { get; set; }

    /// <summary>
    /// 社交平台加密数据（微信绑定电话）
    /// </summary>
    //public string? EncryptedData { get; set; }

    /// <summary>
    /// IV
    /// </summary>
    //public string? IV { get; set; }

    /// <summary>
    /// Token
    /// </summary>
    //public string? Token { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// Mobile
    /// </summary>
    public long Mobile { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Birthday
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 国家
    /// </summary>
    public string? Country { get; set; } = string.Empty;

    /// <summary>
    /// 省
    /// </summary>
    public string? Province { get; set; } = string.Empty;

    /// <summary>
    /// 城市
    /// </summary>
    public string? City { get; set; } = string.Empty;

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 性别(0未知,1男,2女)
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// 身份证号码
    /// </summary>
    public string? IdNumber { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    public string IP { get; set; }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mobile"></param>
    public RegisterRequest(long mobile, string ip)
    {
        Mobile = mobile;
        IP = ip;
    }
}