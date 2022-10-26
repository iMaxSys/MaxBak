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

using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Environment;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Environment.Access;
using Microsoft.Extensions.Logging.Abstractions;

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
    /// 日志工厂
    /// </summary>
    private ILoggerFactory? _loggerFactory;

    /// <summary>
    /// 日志
    /// </summary>
    private ILogger? _logger;

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
    protected IWorkContext? WorkContext => _workContext ??= HttpContext.RequestServices.GetService<IWorkContext>();

    /// <summary>
    /// 获取 <see cref="ILoggerFactory"/> 对象。
    /// </summary>
    protected ILoggerFactory? LoggerFactory
    {
        get { return _loggerFactory ?? (_loggerFactory = HttpContext.RequestServices.GetService<ILoggerFactory>()); }
    }

    /// <summary>
    /// Logger
    /// </summary>
    protected ILogger? Logger { get => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger>()); }

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
    protected Result Result(Enum resultCode, object data)
    {
        if (resultCode.GetHashCode() == MaxCode.Success.GetHashCode())
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
        return MaxCode.Fail.Result();
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <returns></returns>
    protected virtual Result Fail(object data)
    {
        return MaxCode.Fail.Result(data);
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <returns></returns>
    protected virtual Result Success()
    {
        return MaxCode.Success.Result();
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    protected virtual Result Success(object data)
    {
        return MaxCode.Success.Result(data);
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    protected virtual Result<T> Success<T>(T data)
    {
        return MaxCode.Success.Result<T>(data);
    }

    /// <summary>
    /// 拒绝
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    protected virtual Result Deny(object data)
    {
        HttpContext.Response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
        return MaxCode.Unauthorized.Result(data);
    }

    /// <summary>
    /// 获取请求text
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
            throw new MaxException(MaxCode.RequestIsEmpty, HttpStatusCode.BadRequest);

        return text;
    }

    /// <summary>
    /// Member
    /// </summary>
    protected IMember? Member { get => WorkContext?.AccessChain.Member; }
}
