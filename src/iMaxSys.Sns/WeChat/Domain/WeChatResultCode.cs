//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IWeChatEntity.cs
//摘要: 微信实体标识接口
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using System.ComponentModel;

namespace iMaxSys.SDK.Sns.WeChat.Domain
{
    /// <summary>
    /// 微信结果代码
    /// </summary>
    public enum WeChatResultCode
    {
        /// <summary>
        /// 获取微信访问配置异常
        /// </summary>
        [Description("获取微信访问配置异常")]
        GetAccessConfigFail = 300000,
    }
}
