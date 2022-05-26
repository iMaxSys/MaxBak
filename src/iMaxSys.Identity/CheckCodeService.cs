//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CheckCodeService.cs
//摘要: 验证码服务
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Data;
using iMaxSys.Data.Entities;
using iMaxSys.Data.Entities.App;
using iMaxSys.Data.Repositories;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;
using iMaxSys.Identity.Data.Repositories;
using iMaxSys.Max.Common;

namespace iMaxSys.Identity;

/// <summary>
/// 验证码服务
/// </summary>
public class CheckCodeService : ICheckCodeService
{
    private readonly MaxOption _option;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 验证码生成处理
    /// </summary>
    public CheckCodeCreatedHandler? CheckCodeCreatedHandler { get; set; }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="option"></param>
    /// <param name="unitOfWork"></param>
    public CheckCodeService(IOptions<MaxOption> option, IUnitOfWork unitOfWork)
    {
        _option = option.Value;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 校验
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="bizId"></param>
    /// <param name="memberId"></param>
    /// <param name="to"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task CheckAsync(long sid, long bizId, long memberId, string to, string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new MaxException(ResultCode.CheckCodeCantNull);
        }

        var xppSns = await _unitOfWork.GetRepository<XppSns>().FindAsync(sid);

        //此处有意去除应用+租户+业务条件过滤
        var checkCode = await _unitOfWork.GetRepository<CheckCode>().FirstOrDefaultAsync(x => x.XppId == xppSns!.XppId && x.BizId == bizId && x.To == to && x.Status == Status.Enable && x.Expires > DateTime.Now, null, null, false, true);

        //无匹配的验证码
        if (checkCode == null)
        {
            throw new MaxException(ResultCode.CheckCodeNotExists);
        }
        else
        {
            checkCode.CheckCount++;
            if (checkCode.Code == code)
            {
                //验证成功,状态失效
                checkCode.Status = Status.Disable;
            }
            else
            {
                //验证错误
                throw new MaxException(ResultCode.CheckCodeError);
            }
            _unitOfWork.GetRepository<CheckCode>().Update(checkCode);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="tenantId"></param>
    /// <param name="bizId"></param>
    /// <param name="bizName"></param>
    /// <param name="memberId"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public async Task<CheckCodeModel> MakeAsync(long sid, long tenantId, long bizId, string bizName, long memberId, string to)
    {
        //验证码请求频率检查, 如果存在有效的验证码, 则提示请求频率过快
        //bool has = await _checkCodeRepository.AnyAsync(x => (x.To == to || (x.MemberId > 0 && x.MemberId == memberId)) && x.Expires > DateTime.Now);
        bool has = await _unitOfWork.GetRepository<CheckCode>().AnyAsync(x => (x.To == to || (x.MemberId > 0 && x.MemberId == memberId)) && x.Expires > DateTime.Now);

        if (has)
        {
            throw new MaxException(ResultCode.CheckCodeTimeLimit);
        }

        var xppSns = await _unitOfWork.GetRepository<XppSns>().FindAsync(sid);

        //生成验证码发送信息
        string code = Max.Algorithm.CheckCode.Next();

        CheckCode checkCode = new()
        {
            TenantId = tenantId,
            XppId = xppSns!.XppId,
            BizId = bizId,
            Code = code,
            Content = $"验证码为:{code}，{_option.Identity.CheckCodeExpires}分钟内有效，请尽快进行{bizName}",
            CheckCount = 0,
            MemberId = memberId,
            To = to,
            Expires = DateTime.Now.AddMinutes(_option.Identity.CheckCodeExpires),
            Status = Status.Enable
        };

        //保存验证码
        await _unitOfWork.GetRepository<CheckCode>().AddAsync(checkCode);
        await _unitOfWork.SaveChangesAsync();


        CheckCodeModel model = new()
        {
            Code = checkCode.Code,
            Expires = checkCode.Expires,
            BizName = bizName
        };

        //同步或异步
        CheckCodeCreatedHandler?.Invoke(model);

        return model;

    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="tenantId"></param>
    /// <param name="bizId"></param>
    /// <param name="bizName"></param>
    /// <param name="memberId"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    //public async Task<CheckCodeModel> MakeAsync(BizConfig bizConfig, long memberId, string to)
    //{
    //    return await MakeAsync(bizConfig.Sid, bizConfig.TenantId, bizConfig.BizId, bizConfig.BizName, memberId, to);
    //}
}

/// <summary>
/// 验证码生成处理
/// </summary>
/// <param name="model"></param>
/// <returns></returns>
public delegate Task CheckCodeCreatedHandler(CheckCodeModel model);