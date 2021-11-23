using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Services;

/// <summary>
/// 序列号服务
/// </summary>
public interface ISerialService : ISingleton

{
    /// <summary>
    /// 获取下一流水号
    /// </summary>
    /// <param name="id">业务Id</param>
    /// <returns></returns>
    Task<int> Next(string id);
}
