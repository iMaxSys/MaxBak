//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: SnsAuth.cs
//摘要: Sns授权
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-02-02
//----------------------------------------------------------------

using System;
using System.Text;
using System.Collections.Generic;

namespace iMaxSys.SDK.Sns.Domain.Auth
{
    /// <summary>
    /// Sns授权
    /// </summary>
    public class SnsAuth
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public string Code { get; set; }
    }
}
