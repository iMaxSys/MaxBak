//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ExceptionHandlingExtensions.cs
//摘要: ExceptionHandlingExtensions
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Builder;

namespace iMaxSys.Max.Exceptions
{
    /// <summary>
    /// ApplicationBuilder之异常处理扩展
    /// </summary>
    public static class ExceptionHandlingExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, Action<ExceptionHandlingOptions> optionsAction = null)
        {
            var options = new ExceptionHandlingOptions();
            optionsAction(options);
            return app.UseMiddleware<ExceptionHandlingMiddleware>(options);
        }
    }
}