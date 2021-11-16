//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IDependency.cs
//摘要: 依赖标识接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.DependencyInjection
{
    /// <summary>
    /// 标记一个类型为依赖项,使用此接口的依赖项的生命周期默认为Scoped.
    /// </summary>
    public interface IDependency
    {
    }

    /// <summary>
    /// 标记一个类型为依赖项,使用此接口的依赖项生命周期为Transient.
    /// </summary>
    public interface ITransient : IDependency
    {
    }

    /// <summary>
    /// 标记一个类型为依赖项,使用此接口的依赖项生命周期为Singleton.
    /// </summary>
    public interface ISingleton : IDependency
    {
    }

    /// <summary>
    /// IServiceRoot, 用于标识多实现的根接口, 可配合生命周期标识接口使用, 默认Scoped
    /// </summary>
    public interface IServiceRoot
    {
    }
}
