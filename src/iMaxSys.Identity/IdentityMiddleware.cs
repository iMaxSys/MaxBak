//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IdentityMiddleware.cs
//摘要: 身份中间件
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-16
//----------------------------------------------------------------
/*
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.DependencyInjection;

using iMaxSys.Max.Domain;
using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Environment;

namespace iMaxSys.Identity
{

    /// <summary>
    /// 身份中间件
    /// </summary>
    public class IdentityMiddleware
    {
        const string FLAG_TOKEN = "Token";

        private readonly MaxOption _option;
        private readonly RequestDelegate _next;

        public IdentityMiddleware(RequestDelegate next, IOptions<MaxOption> option)
        {
            _next = next;
            _option = option.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            await Access(context);
            await _next(context);
        }

        /// <summary>
        /// 访问控制
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_accessOptions"></param>
        /// <returns></returns>
        public async Task Access(HttpContext context)
        {
            string router = context.Request.Path.Value.ToLower();
            //判断是否需要token
            if ("*" != _option.Identity.OpenRouters && !_option.Identity.OpenRouters.Contains(router) && !context.Request.Headers.ContainsKey(FLAG_TOKEN))
            {
                //Headers中无token
                throw new MaxException(ResultEnum.NeedToken, context.Request.Path.Value);
            }

            if (context.Request.Headers.TryGetValue(FLAG_TOKEN, out StringValues values))
            {
                IWorkContext workContext = context.RequestServices.GetService<IWorkContext>();
                IIdentityService identityService = context.RequestServices.GetService<IIdentityService>();
                //string token = "6a732875346247e298b55f688c4709a3";
                //string token = values[0].ToString();
                workContext.AccessChain = await identityService.CheckAsync(values[0].ToString(), router);
            }
        }
    }
}
*/