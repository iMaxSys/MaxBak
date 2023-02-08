//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: BizConfig.cs
//摘要: 业务配置
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max
{
    /// <summary>
    /// 应用信息
    /// </summary>
    public class Xpp
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 新用户注册是否需要手机号
        /// </summary>
        public bool NeedMobile { get; set; } = true;

        /// <summary>
        /// XppSource
        /// </summary>
        public XppSource Source { get; set; }

        /// <summary>
        /// 第三方平台原始Id(暂不使用)
        /// </summary>
        public string? AccountId { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// AppKey
        /// </summary>
        public string? AppKey { get; set; }

        /// <summary>
        /// Host
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; } = Status.Enable;

        /// <summary>
        /// 应用社交
        /// </summary>
        public virtual ICollection<XppSns>? XppSnses { get; set; }
    }
}