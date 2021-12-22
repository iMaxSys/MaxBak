//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AuthResponse.cs
//摘要: 授权应答
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using System.Text.Json.Serialization;

namespace iMaxSys.SDK.Sns.WeChat.Api.Response
{
    /// <summary>
    /// 授权应答
    /// </summary>
    public class AuthResponse : WeChatResponse
    {
        /// <summary>
        /// SessionKey
        /// </summary>
        /// [JsonProperty(PropertyName = "session_key")]
        [JsonPropertyName("session_key")]
        public string SessionKey { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        //[JsonPropertyName("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// UnionId
        /// </summary>
        //[JsonPropertyName("unionid")]
        public string UnionId { get; set; }
    }
}
