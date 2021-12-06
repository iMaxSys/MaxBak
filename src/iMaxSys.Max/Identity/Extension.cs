//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Extensions.cs
//摘要: Extensions
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System;
using System.Text.Json;

using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Max.Identity
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 获取具体用户
        /// </summary>
        /// <param name="member"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object? GetUser(this IMember member, Type type)
        {
            //if (member.User != null)
            //{
            //    return member.User;
            //}
            //else
            //{
            if (!string.IsNullOrWhiteSpace(member.UserJson))
            {
                return JsonSerializer.Deserialize(member.UserJson, type);
            }
            else
            {
                return null;
            }
            //}
        }

        /// <summary>
        /// 获取具体用户
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member"></param>
        /// <returns></returns>
        public static object? User<T>(this IMember member)
        {
            if (member.User != null)
            {
                return member.User;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(member.UserJson))
                {
                    member.User = JsonSerializer.Deserialize<T>(member.UserJson);
                    return member.User;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}