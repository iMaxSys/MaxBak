//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: 控制器基类.cs
//摘要: MaxController
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-12
//----------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using iMaxSys.Max.Domain;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Environment;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Environment.Access;

namespace iMaxSys.Max.Web.Mvc;

/// <summary>
/// Max控制器基类
/// </summary>
[ApiController]
[Produces("application/json")]
public abstract class MaxController : Controller
{
    /// <summary>
    /// 工作上下文
    /// </summary>
    private IWorkContext? _workContext;

    /// <summary>
    /// Application
    /// </summary>
    public IApplication Application { get => WorkContext!.Application; }

    /// <summary>
    /// Session
    /// </summary>
    public Environment.Access.ISession Session { get => WorkContext!.Session; }

    /// <summary>
    /// 访问凭据串
    /// </summary>
    protected IAccessChain AccessChain { get => WorkContext!.AccessChain; }

    /// <summary>
    /// WorkContext
    /// </summary>
    public IWorkContext? WorkContext => _workContext ??= HttpContext.RequestServices.GetService<IWorkContext>();

    /// <summary>
    /// Result
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static object Result(object result)
    {
        return result;
    }

    /// <summary>
    /// 结果
    /// </summary>
    /// <param name="resultCode"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    //protected Result Result(ResultEnum resultCode, object data)
    //{
    //    if (resultCode == ResultEnum.Success)
    //    {
    //        return Success(data);
    //    }
    //    else
    //    {
    //        return Fail(data);
    //    }
    //}

    /// <summary>
    /// 结果
    /// </summary>
    /// <param name="resultCode"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    protected Result Result(Enum resultCode, object data)
    {
        if (resultCode.GetHashCode() == ResultEnum.Success.GetHashCode())
        {
            return Success(data);
        }
        else
        {
            return Fail(data);
        }
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <returns></returns>
    protected virtual Result Fail()
    {
        return ResultEnum.Fail.Result();
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <returns></returns>
    protected virtual Result Fail(object data)
    {
        return ResultEnum.Fail.Result(data);
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <returns></returns>
    protected virtual Result Success()
    {
        return ResultEnum.Success.Result();
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    protected virtual Result Success(object data)
    {
        return ResultEnum.Success.Result(data);
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    protected virtual Result<T> Success<T>(T data)
    {
        return ResultEnum.Success.Result<T>(data);
    }

    /// <summary>
    /// 拒绝
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    protected virtual Result Deny(object data)
    {
        HttpContext.Response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
        return ResultEnum.Unauthorized.Result(data);
    }

    /// <summary>
    /// 获取请求Json
    /// </summary>
    /// <returns></returns>
    protected async Task<string> GetRequestText()
    {
        string text = string.Empty;
        Request.EnableBuffering();

        //获取Body中的原始json
        if (Request.Body.CanSeek)
        {
            Request.Body.Position = 0;
            using StreamReader reader = new StreamReader(Request.Body);
            text = await reader.ReadToEndAsync();
        }

        //检查消息体是否为空
        if (!Request.Body.CanSeek || string.IsNullOrWhiteSpace(text))
            throw new MaxException(ResultEnum.RequestIsEmpty, HttpStatusCode.BadRequest);

        return text;
    }

    /// <summary>
    /// 获取member,便于本地调试用,可不用
    /// </summary>
    protected virtual IMember GetMember()
    {
        return WorkContext.AccessChain.Member;
    }
}
