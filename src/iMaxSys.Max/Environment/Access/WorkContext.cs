//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: WorkContext.cs
//摘要: WorkContext
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------
 
using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Max.Environment.Access;

/// <summary>
/// WorkContext
/// </summary>
public class WorkContext : IWorkContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApplication _application;
    private readonly ISession _session;

    private IAccessChain? _accessChain;

    /// <summary>
    /// 访问链
    /// </summary>
    public IAccessChain? AccessChain
    {
        get
        {
            return _accessChain;
        }
        set
        {
            _accessChain = value;
        }
    }

    /// <summary>
    /// Application
    /// </summary>
    public IApplication Application { get => _application; }

    /// <summary>
    /// 会话
    /// </summary>
    public ISession Session { get => _session; }

    /// <summary>
    /// Xpp information
    /// </summary>
    public Xpp Xpp { get; set; } = new();

    /// <summary>
    /// Tenant information
    /// </summary>
    public Tenant Tenant { get; set; } = new();

    /// <summary>
    /// IP
    /// </summary>
    public string? IP { get; set; }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="application"></param>
    /// <param name="session"></param>
    public WorkContext(IHttpContextAccessor httpContextAccessor, IApplication application, ISession session, IOptions<MaxOption> option)
    {
        _httpContextAccessor = httpContextAccessor;
        _application = application;
        _session = session;
        IP = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
    }

    /// <summary>
    /// Get service of type
    /// </summary>
    /// <typeparam name="T">The type of service object to get.</typeparam>
    /// <returns></returns>
    public T? GetService<T>()
    {
        var provider = _httpContextAccessor?.HttpContext?.RequestServices;
        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        return provider.GetService<T>();
    }

    /// <summary>
    /// Get service of type
    /// </summary>
    /// <typeparam name="T">The type of service object to get.</typeparam>
    /// <returns></returns>
    public T GetRequiredService<T>()
    {
        var provider = _httpContextAccessor?.HttpContext?.RequestServices;
        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        return (T)provider.GetRequiredService(typeof(T));
    }
}