//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MemcacheOption.cs
//摘要: 缓存选项
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Options
{
    /// <summary>
    /// 缓存选项
    /// </summary>
    public class CachingOption
    {
        /// <summary>
        /// Type[0:max, 1:redis]
        /// </summary>
        public int Type { get; set; } = 0;

        /// <summary>
        /// Connection
        /// </summary>
        public string Connection { get; set; } = String.Empty;

        /// <summary>
        /// 过期时间(分钟)
        /// </summary>
        public int Expires { get; set; } = 60;
    }
}
