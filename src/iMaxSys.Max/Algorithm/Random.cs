//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MaxRandom.cs
//摘要: 随机类
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using System;

namespace iMaxSys.Max.Algorithm
{
    /// <summary>
    /// 随机类
    /// </summary>
    public static class MaxRandom
    {
        /// <summary>
        /// 随机整数
        /// </summary>
        /// <returns></returns>
        public static double Next()
        {
            return new Random().NextDouble();
        }
    }
}
