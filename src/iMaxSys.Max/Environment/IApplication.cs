//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IApplication.cs
//摘要: IApplication
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-15
//----------------------------------------------------------------

using System.Threading.Tasks;

using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Environment
{
    /// <summary>
    /// IApplication
    /// </summary>
    public interface IApplication : ISingleton

    {
        T Get<T>(string key);

        Task<T> GetAsync<T>(string key);

        void Set(string key, object data);

        Task SetAsync(string key, object data);
    }
}
