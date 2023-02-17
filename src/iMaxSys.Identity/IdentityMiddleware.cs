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

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Environment.Access;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Identity.Common;
using iMaxSys.Core.Services;

namespace iMaxSys.Identity;

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

    public async Task InvokeAsync(HttpContext context)
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
        string? router = context.Request.Path.Value;

        //空则pass
        if (string.IsNullOrWhiteSpace(router))
        {
            return;
        }

        IWorkContext workContext = context.RequestServices.GetRequiredService<IWorkContext>();
        workContext.Xpp = await context.RequestServices.GetRequiredService<ICoreService>().GetXppAsync(_option.XppId);

        //判断是否需要token
        if (!_option.Identity.OpenRouters.Contains(router.ToLower()) && !context.Request.Headers.ContainsKey(FLAG_TOKEN))
        {
            //Headers中无token
            throw new MaxException(ResultCode.NeedToken, router);
        }

        if (context.Request.Headers.TryGetValue(FLAG_TOKEN, out StringValues values))
        {
            IMemberService memberService = context.RequestServices.GetRequiredService<IMemberService>();
            workContext.AccessChain = await memberService.CheckAsync($"{values[0]}", router.ToLower());
        }
    }
}