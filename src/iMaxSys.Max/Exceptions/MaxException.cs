//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MaxException.cs
//摘要: MaxException
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System;
using System.Net;

using iMaxSys.Max.Extentions;

namespace iMaxSys.Max.Exceptions
{
    public class MaxException : Exception
    {
        /// <summary>
        /// Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Info
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// InnerMaxException
        /// </summary>
        public MaxException? InnerMaxException { get; set; }

        /// <summary>
        /// HttpStatusCode
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;


        public MaxException(Enum value, string more)
        {
            Code = value.GetHashCode();
            Info = $"{value.GetDescription()}[{more}]";
        }

        public MaxException(Enum value)
        {
            Code = value.GetHashCode();
            Info = value.GetDescription();
        }

        public MaxException(Enum value, HttpStatusCode httpStatusCode)
        {
            Code = value.GetHashCode();
            Info = value.GetDescription();
            HttpStatusCode = httpStatusCode;
        }

        public MaxException(int code, string info)
        {
            Code = code;
            Info = info;
        }

        public MaxException(int code, string info, HttpStatusCode httpStatusCode)
        {
            Code = code;
            Info = info;
            HttpStatusCode = httpStatusCode;
        }

        public MaxException(Exception ex, Enum value) : base(value.GetDescription(), ex is MaxException ? null : ex)
        {
            Code = value.GetHashCode();
            Info = value.GetDescription();
            InnerMaxException = ex is MaxException ? ex as MaxException : null;
        }
    }
}
