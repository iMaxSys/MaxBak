//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: WeChatResponse.cs
//摘要: 微信应答
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using iMaxSys.SDK.Sns.Api.Response;

namespace iMaxSys.SDK.Sns.WeChat.Api.Response
{
    /// <summary>
    /// 微信应答
    /// </summary>
    public class WeChatResponse : SnsResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; }
    }
}
