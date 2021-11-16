//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: UnderscoreNamesContractResolver.cs
//摘要: UnderscoreNamesContractResolver
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System.Text.Json;
using System.Text.RegularExpressions;

//using Newtonsoft.Json.Serialization;

//namespace iMaxSys.Max.Json.Resolver
//{
//    /// <summary>
//    /// 下划线解析
//    /// </summary>
//    public class UnderscoreNamesContractResolver : DefaultContractResolver
//    {
//        //public UnderscoreNamesContractResolver() : base()
//        //{
//        //}

//        protected override string ResolvePropertyName(string propertyName)
//        {
//            //return Regex.Replace(propertyName, @"((?<=.)[A-Z][a-z]*)|((?<=[a-zA-Z])\d+)", @"_$1$2").ToLower();
//            return Regex.Replace(propertyName, @"((?<=.)[A-Z][a-z]*)", @"_$1").ToLower();
//        }
//    }
//}
