//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: NetworkOption.cs
//摘要: 网络配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Options
{
    /// <summary>
    /// 网络配置
    /// </summary>
    public class NetworkOption
    {
        /// <summary>
        /// 数据中心标识
        /// </summary>
        public int DataCenterId { get; set; } = 0;

        /// <summary>
        /// 服务器标识
        /// </summary>
        public int ServerId { get; set; } = 0;
    }
}
