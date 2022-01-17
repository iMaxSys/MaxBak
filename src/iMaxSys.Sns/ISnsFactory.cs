//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ISnsFactory.cs
//摘要: 社交服务工厂接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using iMaxSys.Sns.Common;

namespace iMaxSys.Sns;

public interface ISnsFactory : IDependency
{
    ISns GetService(Platform platform);
}