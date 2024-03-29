﻿//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CheckCodeRepository.cs
//摘要: 验证码仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Data.Repositories;
using iMaxSys.Identity.Data.Entities;


namespace iMaxSys.Identity.Data.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    /// <summary>
    /// 更新索引
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="parentId"></param>
    /// <param name="start"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    //int UpdateIndex(long tenantId, long parentId, int start, int step);
}

