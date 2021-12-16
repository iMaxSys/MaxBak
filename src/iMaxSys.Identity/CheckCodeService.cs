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



using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using iMaxSys.Data;
using iMaxSys.Data.Entities;
using iMaxSys.Max.Domain;
using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Identity.Data.Models;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Repositories;
using iMaxSys.Data.Entities.App;

namespace iMaxSys.Identity
{
    /// <summary>
    /// 验证码服务
    /// </summary>
    public class CheckCodeService : ICheckCodeService
    {
        private readonly MaxOption _option;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IMaxIdentityRepository _maxIdentityRepository;
        private readonly ICheckCodeRepository _checkCodeRepository;

        public CheckCodeService(IOptions<MaxOption> option, IUnitOfWork<MaxIdentityContext> unitOfWork)
        {
            _option = option.Value;
            _unitOfWork = unitOfWork;
            _checkCodeRepository = _unitOfWork.GetCustomRepository<ICheckCodeRepository>();
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
                throw new MaxException(ResultEnum.CheckCodeCantNull);
            }

            //定制仓储
            var repository = _unitOfWork.GetCustomRepository<ICheckCodeRepository>();


            var xppSns = await _unitOfWork.GetRepository<XppSns>().FindAsync(sid);

            //此处有意去除应用+租户+业务条件过滤
            var checkCode = await _unitOfWork.GetRepository<CheckCode>().GetFirstOrDefaultAsync(x => x.XppId == xppSns!.XppId && x.BizId == bizId && x.To == to && x.Status == Status.Enable && x.Expires > DateTime.Now, null, null, false, true);

            //无匹配的验证码
            if (checkCode == null)
            {
                throw new MaxException(ResultEnum.CheckCodeNotExists);
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
                    throw new MaxException(ResultEnum.CheckCodeError);
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
        public async Task<CheckCodeResult> MakeAsync(long sid, long tenantId, long bizId, string bizName, long memberId, string to)
        {
            //验证码请求频率检查, 如果存在有效的验证码, 则提示请求频率过快
            //bool has = await _checkCodeRepository.AnyAsync(x => (x.To == to || (x.MemberId > 0 && x.MemberId == memberId)) && x.Expires > DateTime.Now);
            bool has = await _unitOfWork.GetRepository<CheckCode>().AnyAsync(x => (x.To == to || (x.MemberId > 0 && x.MemberId == memberId)) && x.Expires > DateTime.Now);

            if (has)
            {
                throw new MaxException(ResultEnum.CheckCodeTimeLimit);
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

            return new CheckCodeResult
            {
                Code = code,
                Expires = _option.Identity.CheckCodeExpires,
                BizName = bizName
            };
        }
    }
}