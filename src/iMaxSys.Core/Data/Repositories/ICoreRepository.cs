//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IIDentityRepository.cs
//摘要: 身份仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;

namespace iMaxSys.Core.Data.Repositories;

public interface ICoreRepository<T> : IRepository<T> where T : Entity
{
}

public interface ICoreReadOnlyRepository<T> : IReadOnlyRepository<T> where T : Entity
{
}
