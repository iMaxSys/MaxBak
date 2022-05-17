//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IDbConextBase.cs
//摘要: IDbConextBase 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-05-16
//----------------------------------------------------------------

namespace iMaxSys.Data.DbContexts;

/// <summary>
/// 只读上下文接口标识
/// </summary>
public interface IReadOnlyDbContext : IDbConextBase
{
}