﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Result.cs
//摘要: Result
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------


namespace iMaxSys.Max.Domain
{
    /// <summary>
    /// System Result
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 成功标志
        /// </summary>
        public bool Success { get; set; } = false;
        /// <summary>
        /// 代码
        /// </summary>
        public int Code { get; set; } = ResultEnum.Fail.GetHashCode();
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 详情
        /// </summary>
        public string Detail { get; set; } = string.Empty;
        /// <summary>
        /// 数据
        /// </summary>
        public virtual object Data { get; set; } = null;
    }

    /// <summary>
    /// System Result
    /// </summary>
    public class Result<T>
    {
        /// <summary>
        /// 成功标志
        /// </summary>
        public bool Success { get; set; } = false;
        /// <summary>
        /// 代码
        /// </summary>
        public int Code { get; set; } = ResultEnum.Fail.GetHashCode();
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 详情
        /// </summary>
        public string Detail { get; set; } = string.Empty;
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; } = default;
    }
}