//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: WeChatResponse.cs
//摘要: 微信应答
//说明: Newton替换为System.Text.Json
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using System;
using System.Text.Json;
using System.Threading.Tasks;

using iMaxSys.Max.Domain;
using iMaxSys.Max.Net.Http;
using iMaxSys.Max.Exceptions;

using iMaxSys.SDK.Sns.WeChat.Domain;
using iMaxSys.SDK.Sns.WeChat.Api.Request;
using iMaxSys.SDK.Sns.WeChat.Api.Response;

namespace iMaxSys.SDK.Sns.WeChat.Api
{
    /// <summary>
    /// WeChatClient
    /// </summary>
    public class WeChatClient : IWeChatClient
    {
        private readonly IHttpService _httpService;

        public WeChatClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<T> ExecuteAsync<T>(WeChatRequest request, WeChatResultCode code)
        {
            try
            {
                if (request.Method == "POST")
                {

                    return await _httpService.PostAsync<T>(request.Url);
                }
                else
                {
                    return await _httpService.GetAsync<T>(request.Url);
                }
            }
            catch (Exception ex)
            {
                throw new MaxException(ex, code);
            }


            //包含errcode表示失败/异常
            //if (response.Contains("errcode"))
            //{
            //    WeChatResponse weChatResponse = JsonSerializer.Deserialize<WeChatResponse>(response);

            //    result = new Result
            //    {
            //        Success = false,
            //        Code = weChatResponse.ErrCode,
            //        Message = weChatResponse.ErrMsg
            //    };

            //    throw new MaxException(code, response);
            //}
            //else
            //{
            //    return JsonSerializer.Deserialize<T>(response, _options);
            //}
        }
    }

    /*
    /// <summary>
    /// 微信API访问客户端
    /// </summary>
    public static class WeChatClient
    {
        private static readonly JsonSerializerOptions _options;

        static WeChatClient()
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteAsync<T>(WeChatRequest request, WeChatResultCode code)
        {
            Result result;
            string response;

            try
            {
                if (request.Method == "POST")
                {

                    response = await HttpClient.Post(request.Url, null);
                }
                else
                {
                    response = await HttpClient.Get(request.Url, null);
                }
            }
            catch (Exception ex)
            {
                throw new MaxException(ex, code);
            }


            //包含errcode表示失败/异常
            if (response.Contains("errcode"))
            {
                WeChatResponse weChatResponse = JsonSerializer.Deserialize<WeChatResponse>(response);

                result = new Result
                {
                    Success = false,
                    Code = weChatResponse.ErrCode,
                    Message = weChatResponse.ErrMsg
                };

                throw new MaxException(code, response);
            }
            else
            {
                return JsonSerializer.Deserialize<T>(response, _options);
            }
        }
    }
    */
}
