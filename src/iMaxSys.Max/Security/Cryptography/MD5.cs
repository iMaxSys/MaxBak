//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Md5.cs
//摘要: Md5
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
    /// MD5
    /// </summary>
    public static class MD5
    {
        /// <summary>
        /// MD5 Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Hash(string source)
        {
            return string.Join("", System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(source)).Select(x => x.ToString("x2")));
        }

        /// <summary>
        /// Verify
        /// </summary>
        /// <param name="source"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool Verify(string source, string hash)
        {
            string sourceHash = Hash(source);
            return 0 == StringComparer.OrdinalIgnoreCase.Compare(sourceHash, hash);
        }
    }
}