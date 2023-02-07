//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AppMiddleWare.cs
//摘要: 应用中间件
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-05-26
//----------------------------------------------------------------

using iMaxSys.Max.Environment.Access;

namespace iMaxSys.Max.Middlewares;

/// <summary>
/// 应用中间件
/// </summary>
public class AppMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly MaxOption _option;
    private readonly IWorkContext _workContext;

    public AppMiddleWare(RequestDelegate next, IOptions<MaxOption> option, IWorkContext workContext)
    {
        _next = next;
        _option = option.Value;
        _workContext = workContext;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        SetXppInfo(context);
        await _next(context);
    }

    private void SetXppInfo(HttpContext context)
    {
        //_workContext.Xpp = new XppInfo();
    }
}

