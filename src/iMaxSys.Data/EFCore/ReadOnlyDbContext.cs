//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ReadOnlyDbContext.cs
//摘要: 只读仓储接口 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-05-07
//----------------------------------------------------------------

using iMaxSys.Max.Collection;
using iMaxSys.Data.Entities;

namespace iMaxSys.Data
{
	public class ReadOnlyDbContext : DbContext, IReadOnlyDbContext
	{
		public ReadOnlyDbContext()
		{
		}
	}
}