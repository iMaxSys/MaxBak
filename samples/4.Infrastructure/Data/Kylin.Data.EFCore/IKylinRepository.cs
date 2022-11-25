//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ICareOneRepository.cs
//摘要: ICareOneRepository 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using System;
using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;

namespace Kylin.Data.EFCore;

public interface IKylinRepository<T> : IRepository<T> where T : Entity
{
}

