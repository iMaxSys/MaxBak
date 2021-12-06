//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IdentityOption.cs
//摘要: IdentityOption
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Options
{
    /// <summary>
    /// IdentityOption
    /// </summary>
    public class IdentityOption
    {
        /// <summary>
        /// 连接
        /// </summary>
        public string Connection { get; set; } = string.Empty;

        /// <summary>
        /// 全局标志,默认否
        /// </summary>
        /// public bool Global { get; set; } = false;

        /// <summary>
        /// 过期分钟数,默认4320(3天)
        /// </summary>
        public int Expires { get; set; } = 4320;

        /// <summary>
        /// 刷新分钟数,默认30分钟
        /// </summary>
        public int Refresh { get; set; } = 30;

        /// <summary>
        /// 验证码过期时间
        /// </summary>
        public int CheckCodeExpires { get; set; } = 5;

        /// <summary>
        /// 验证模式,默认0-简单模式
        /// 0:简单模式(除OpenRouters,仅需要登录验证)
        /// 1:严格验证(除OpenRouters,还需要进行角色权限验证)
        /// </summary>
        public int CheckMode = 0;

        /// <summary>
        /// 开放API,默认全开放
        /// </summary>
        public string OpenRouters { get; set; } = "*";
    }
}