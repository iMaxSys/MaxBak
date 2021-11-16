//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CheckCode.cs
//摘要: 验证码类
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using System;

using iMaxSys.Max.Extentions;

namespace iMaxSys.Max.Algorithm
{
    /// <summary>
    /// 验证码类
    /// </summary>
    public static class CheckCode
    {
        const int LENGTH = 6;

        /// <summary>
        /// 获取新数字验证码
        /// </summary>
        /// <returns></returns>
        public static string Next(int length = LENGTH)
        {
            return new Random().Next().ToString("000000").Left(length > LENGTH ? LENGTH : length);
        }
    }
}
