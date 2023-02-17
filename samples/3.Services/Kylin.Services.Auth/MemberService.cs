// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MemberService.cs
//摘要: 用户服务
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-15
//----------------------------------------------------------------

using AutoMapper;

using iMaxSys.Data;
using iMaxSys.Identity;
using iMaxSys.Identity.Models;
using iMaxSys.Max.Identity.Domain;
using Kylin.Data.Models;
using Kylin.Data.EFCore.Contexts;
using Kylin.Services.Auth.Common;
using Kylin.Services.Auth.Models;

using DbCustomer = Kylin.Data.Models.Auth.Customer;

namespace Kylin.Services.Auth;

/// <summary>
/// 用户服务
/// </summary>
public class MemberService : IMemberService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MemberService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 检查
    /// </summary>
    /// <param name="key"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task CheckAsync(string key, int type = 0)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IUser?> GetAsync(long id, int type = 0)
    {
        IUser? user = null;
        switch ((UserType)type)
        {
            case UserType.Customer:
                DbCustomer? dbCustomer = await _unitOfWork.GetReadOnlyRepository<DbCustomer>().FirstOrDefaultAsync(x => x.Id == id);
                user = _mapper.Map<Customer>(dbCustomer);
                return user;
            default:
                break;
        }
        throw new NotImplementedException();
    }

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="key"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IUser?> GetAsync(string key, int type = 0)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Type GetType(int type = 0)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IUser?> LoginAsync(long userId, int type)
    {
        IUser? user = null;
        switch ((UserType)type)
        {
            case UserType.Customer:
                DbCustomer? dbCustomer = await _unitOfWork.GetReadOnlyRepository<DbCustomer>().FirstOrDefaultAsync(x=>x.Id == userId);
                user = _mapper.Map<Customer>(dbCustomer);
                return user;
            default:
                break;
        }
        throw new NotImplementedException();
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="registerModel"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IUser?> RegisterAsync(RegisterModel registerModel)
    {
        throw new NotImplementedException();
    }
}