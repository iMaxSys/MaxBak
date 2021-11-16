//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: LoggingOption.cs
//摘要: LoggingOption
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Options
{
    /// <summary>
    /// 日志配置
    /// </summary>
    public class LoggingOption
    {
        /// <summary>
        /// Connection
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// Mode
        /// (0:file,1:database,2:remote)
        /// </summary>
        public int Mode { get; set; } = 0;

        /// <summary>
        /// 等级
        /// (0:Trace,1:Debug,2:Information,3:Warning,4:Error,5:Critical,6:None)
        /// </summary>
        public int Level { get; set; } = 4;

        /// <summary>
        /// 文件路径
        /// Mode为0时的输出路径
        /// </summary>
        public string FilePath { get; set; } = "Log";

        /// <summary>
        /// Redis的key
        /// Mode为2的时候字典Key
        /// </summary>
        public string RedisKey { get; set; } = "Log";

        /// <summary>
        /// 任务执行时间间隔，默认30秒
        /// </summary>
        public int Interval { get; set; } = 30;
    }
}
