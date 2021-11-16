//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IdentityOption.cs
//摘要: IdentityOption
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Options
{
    /// <summary>
    /// IdentityOption
    /// </summary>
    public class MessageOption
    {
        /// <summary>
        /// 连接
        /// </summary>
        public string Connection { get; set; } = null;

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; } = 3;

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool Enable { get; set; } = true;
    }
}