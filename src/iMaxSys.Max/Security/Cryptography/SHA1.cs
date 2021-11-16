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
    public static class Sha1
    {
        /// <summary>
        /// SHA1 Hash
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Hash(string source)
        {
            byte[] bout = (new SHA1CryptoServiceProvider()).ComputeHash(UTF8Encoding.Default.GetBytes(source));
            return BitConverter.ToString(bout).Replace("-", "");
        }
    }
}
