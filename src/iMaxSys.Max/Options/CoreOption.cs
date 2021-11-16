//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CoreOption.cs
//摘要: CoreOption
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Options
{
    public class CoreOption
    {
        /// <summary>
        /// 连接
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// 数据库类型
        /// 0:MariaDB,1:MySQL,2:SqlServer,3:Oracle,4:Sybase
        /// </summary>
        public int Type { get; set; }
    }
}
