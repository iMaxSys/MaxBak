//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ExceptionHandlingMiddleware.cs
//摘要: ExceptionHandlingMiddleware
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Http;

using iMaxSys.Max.Domain;

namespace iMaxSys.Max.Exceptions
{
    /// <summary>
    /// 异常处理选项
    /// </summary>
    public class ExceptionHandlingOptions
    {
        /// <summary>
        /// 默认代码
        /// </summary>
        public int DefaultCode { get; set; } = 999999;
        /// <summary>
        /// 默认消息
        /// </summary>
        public string DefaultMessage { get; set; } = "系统异常";
    }

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ExceptionHandlingOptions _options;

        static JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, Encoder = JavaScriptEncoder.Create(allowedRanges: UnicodeRanges.All) };

        public ExceptionHandlingMiddleware(RequestDelegate next, ExceptionHandlingOptions options)
        {
            _next = next;
            _options = options ?? new ExceptionHandlingOptions();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Result result;

            if (exception is MaxException ex)
            {
                result = new Result
                {
                    Code = ex.Code,
                    Message = ex.Info,
                    Detail = $"{GetMaxDetail(ex)}{GetDetail(ex.InnerException)}"
                };
                context.Response.StatusCode = (int)ex.HttpStatusCode;
            }
            else
            {
                result = new Result
                {
                    Code = _options.DefaultCode,
                    Message = _options.DefaultMessage,
                    Detail = GetDetail(exception)
                };
            }

            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result, _jsonSerializerOptions));
        }

        /// <summary>
        /// 获取MaxDetail
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string GetMaxDetail(MaxException ex)
        {
            return null == ex ? "" : $"{ex.Code}:{ex.Info};{(null == ex.InnerMaxException ? "" : GetMaxDetail(ex.InnerMaxException))}";
        }

        /// <summary>
        /// 获取Detail
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string GetDetail(Exception ex)
        {
            return null == ex ? "" : $"[message:{ex.Message};inner:{ex.InnerException?.Message};source:{ex.Source};trace:{ex.StackTrace}]";
        }
    }
}