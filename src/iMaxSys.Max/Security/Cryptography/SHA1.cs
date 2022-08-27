//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: SHA1.cs
//摘要: SHA1
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System;
using System.Text;
using System.Security.Cryptography;

namespace iMaxSys.Max.Security.Cryptography
{
    /// <summary>
    /// SHA1
    /// </summary>
    public static class SHA1
    {
        /// <summary>
        /// 用SHA1加密字符串
        /// </summary>
        /// <param name="source">要扩展的对象</param>
        /// <param name="isReplace">是否替换掉加密后的字符串中的"-"字符</param>
        /// <param name="isToLower">是否把加密后的字符串转小写</param>
        /// <returns></returns>
        public static string Hash(this string source, bool isReplace = true, bool isToLower = false)
        {
            string shastring = BitConverter.ToString(System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(source)));
            if (isReplace)
            {
                shastring = shastring.Replace("-", "");
            }
            if (isToLower)
            {
                shastring = shastring.ToLower();
            }
            return shastring;
        }
    }
}