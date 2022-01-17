//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ResultEnum.cs
//摘要: 数据访问结果枚举
//说明: 102
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Data.Common;

public enum ResultCode
{
    /// <summary>
    /// 无法获取范型仓储
    /// </summary>
    [Description("无法获取范型仓储")]
    CantGetRepository = 102000,

    /// <summary>
    /// 无法定制范型仓储
    /// </summary>
    [Description("无法获取定制仓储")]
    CantGetCustomRepository = 102001,
}