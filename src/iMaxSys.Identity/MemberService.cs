//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMemberService.cs
//摘要: 成员服务接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Identity;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Repositories;
using iMaxSys.Identity.Data.Entities;
using DbMember = iMaxSys.Identity.Data.Entities.Member;

namespace iMaxSys.Identity
{
    /// <summary>
    /// MemberService
    /// </summary>
    public class MemberService : ServiceBase, IMemberService
    {
        /// <summary>
        /// 用户提供者
        /// </summary>
        private readonly IUserProvider _userProvider;

        #region 构造

        public MemberService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork<MaxIdentityContext> unitOfWork, IUserProvider userProvider) : base(mapper, option, unitOfWork)
        {
            _userProvider = userProvider;
        }

        #endregion

        #region GetAccessChainAsync

        /// <summary>
        /// GetAccessChainAsync
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IAccessChain?> GetAccessChainAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            IMemberRepository repository = UnitOfWork.GetCustomRepository<IMemberRepository>();

            //先按Token获取uid
            IAccessSession? access = await repository.GetAccessSessionAsync(token);

            if (access == null)
            {
                return null;
            }

            //按mid获取member
            IMember? member = null;
            if (access.MemberId.HasValue)
            {
                member = await repository.ReadAsync(access.MemberId.Value);

                if (member != null)
                {
                    var type = _userProvider.GetType(member.Type);
                    if (type != null)
                    {
                        member.User = member.GetUser(type);
                    }
                }
            }

            return new AccessChain
            {
                AccessSession = access,
                Member = member
            };
        }

        #endregion

        #region AddAsync

        /// <summary>
        /// Add member
        /// </summary>
        /// <param name="model"></param>
        /// <param name="xppId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IMember> AddAsync(MemberModel model, long xppId, long roleId)
        {
            DbMember dbMember = new();
            SetNewMember(model, xppId, roleId, dbMember);
            await UnitOfWork.GetCustomRepository<IMemberRepository>().AddAsync(dbMember);
            await UnitOfWork.SaveChangesAsync();
            return Mapper.Map<IMember>(dbMember);
        }

        #endregion

        #region Remove

        /// <summary>
        /// 移除成员
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public async Task RemoveAsync(long memberId)
        {
            await UnitOfWork.GetCustomRepository<IMemberRepository>().RemoveAsync(memberId);
            await UnitOfWork.SaveChangesAsync();
        }

        #endregion

        #region GetAsync

        /// <summary>
        /// Get member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IMember?> GetAsync(long id)
        {
            var member = await UnitOfWork.GetCustomRepository<IMemberRepository>().FindAsync(id);
            if (member != null)
            {
                return Mapper.Map<IMember>(member);
            }
            else
            {
                throw new MaxException(ResultEnum.MemberNotExsits);
            }
        }

        #endregion

        #region UpdateAsync

        /// <summary>
        /// Get member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IMember?> UpdateAsync(MemberModel model)
        {
            //成员id判空
            if (!model.Id.HasValue)
            {
                throw new MaxException(ResultEnum.MemberIdCantNull);
            }

            IMemberRepository respoitory = UnitOfWork.GetCustomRepository<IMemberRepository>();
            var member = await respoitory.FindAsync(model.Id);
            if (member == null)
            {
                throw new MaxException(ResultEnum.MemberNotExsits);
            }

            SetMember(model, member);
            UnitOfWork.GetCustomRepository<IMemberRepository>().Update(member);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<IMember>(member);
        }

        #endregion

        #region RefreshAsync

        /// <summary>
        /// 刷新member缓存
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public async Task RefreshAsync(long memberId)
        {
            IMember? member = await GetAsync(memberId);
            if (member != null)
            {
                await RefreshAsync(member);
            }
        }

        /// <summary>
        /// 刷新member缓存
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task RefreshAsync(IMember member)
        {
            await UnitOfWork.GetCustomRepository<IMemberRepository>().RefreshAsync(member, DateTime.Now.AddMinutes(Option.Identity.Expires));
        }

        #endregion

        #region RefreshAcceeChainAsync

        /// <summary>
        /// 刷新AcceeChain缓存
        /// </summary>
        /// <param name="oldToken"></param>
        /// <param name="accessChain"></param>
        /// <returns></returns>
        public async Task RefreshAcceeChainAsync(string oldToken, IAccessChain accessChain)
        {
            await UnitOfWork.GetCustomRepository<IMemberRepository>().RefreshAccessChainAsync(oldToken, accessChain, DateTime.Now.AddMinutes(Option.Identity.Expires));
        }

        #endregion

        #region SetMember

        /// <summary>
        /// 使用MemberModel设置DbMember
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dbMember"></param>
        private static void SetMember(MemberModel model, DbMember dbMember)
        {
            dbMember.UserId = model.ExtId ?? dbMember.UserId;
            dbMember.Avatar = model.Avatar;
            dbMember.Birthday = model.Birthday;
            dbMember.DepartmentId = model.DepartmentId;
            dbMember.Email = model.Email;
            dbMember.End = model.End;
            dbMember.Gender = model.Gender;
            dbMember.IsOfficial = model.IsOfficial;
            dbMember.Mobile = model.Mobile;
            dbMember.Name = model.Name;
            dbMember.QuickCode = model.QuickCode;
            dbMember.NickName = model.NickName;
            dbMember.Start = model.Start;
            dbMember.Status = model.Status;
            dbMember.LoginName = model.UserName ?? dbMember.LoginName;
            dbMember.Type = model.Type;
        }

        /// <summary>
        /// 使用MemberModel设置DbMember
        /// </summary>
        /// <param name="model"></param>
        /// <param name="xppId"></param>
        /// <param name="roleId"></param>
        /// <param name="dbMember"></param>
        private static void SetNewMember(MemberModel model, long xppId, long roleId, DbMember dbMember)
        {
            SetMember(model, dbMember);

            //注册or新增信息
            dbMember.TenantId = model.TenantId;

            var now = DateTime.Now;
            dbMember.JoinTime = now;
            dbMember.JoinIp = model.IP;
            dbMember.LastLogin = now;
            dbMember.LastIp = model.IP;

            dbMember.Salt = Guid.NewGuid().ToString().Replace("-", "");

            //角色信息
            RoleMember roleMember = new()
            {
                XppId = xppId,
                RoleId = roleId,
                Status = Status.Enable,
                TenantId = model.TenantId
            };

            dbMember.RoleMembers = new List<RoleMember>
            {
                roleMember
            };
        }

        /// <summary>
        /// 获取真实会员信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private async Task<IUser?> GetUserAsync(long id, int type)
        {
            if (id > 0 && _userProvider != null)
            {
                return await _userProvider.GetAsync(id, type);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取真实会员信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private async Task<IUser?> GetUserAsync(string mobile, int type)
        {
            if (!string.IsNullOrWhiteSpace(mobile) && _userProvider != null)
            {
                return await _userProvider.GetAsync(mobile, type);
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}