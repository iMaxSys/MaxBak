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

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 用户注册模型
/// </summary>
public class RegisteModel
{
    /// <summary>
    /// Sid
    /// </summary>
    public long XppSnsId { get; set; }

    /// <summary>
    /// 注册方式
    /// </summary>
    public int Way { get; set; }

    /// <summary>
    /// 用户类型
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// MemberId
    /// </summary>
    public long MemberId { get; set; }

    /// <summary>
    /// OpenId
    /// </summary>
    public string OpenId { get; set; } = string.Empty;

    /// <summary>
    /// 社交平台code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 验证码
    /// </summary>
    public string CheckCode { get; set; } = string.Empty;

    /// <summary>
    /// 社交平台加密数据（微信绑定电话）
    /// </summary>
    public string EncryptedData { get; set; } = string.Empty;

    /// <summary>
    /// IV
    /// </summary>
    public string IV { get; set; } = string.Empty;

    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// PhoneNumber
    /// </summary>
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// 国家
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// 省
    /// </summary>
    public string Province { get; set; } = string.Empty;

    /// <summary>
    /// 城市
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// 性别(0未知,1男,2女)
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    public string IP { get; set; } = string.Empty;
}