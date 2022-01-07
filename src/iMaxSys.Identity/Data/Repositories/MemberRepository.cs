//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MemberRepository.cs
//摘要: 成员仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories.EFCore;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;
using DbMember = iMaxSys.Identity.Data.Entities.Member;

namespace iMaxSys.Identity.Data.Repositories
{
    /// <summary>
    /// 成员仓储
    /// </summary>
    public class MemberRepository : EfRepository<DbMember>, IMemberRepository
    {
        protected const string TAG = "id:";
        protected const string TAG_ACCESS = "a:";
        protected const string TAG_MEMBER = "u:";
        protected const string TAG_MENU = "m:";

        protected const string TAG_TENANT_MENU = $"{TAG}{TAG_MENU}";
        protected const string TAG_ACCESS_SECTION = $"{TAG}{TAG_ACCESS}";
        protected const string TAG_MEMBER_SECTION = $"{TAG}{TAG_MEMBER}";

        private readonly IIdentityCache _identityCache;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identityCache"></param>
        public MemberRepository(MaxIdentityContext context, IIdentityCache identityCache) : base(context)
        {
            _identityCache = identityCache;
        }

        /// <summary>
        /// 获取访问Session
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        public async Task<IAccessSession?> GetAccessSessionAsync(string token)
        {
            return await _identityCache.GetAsync<AccessSession>($"{TAG_MEMBER_SECTION}{token}", true);
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="memberId">成员id</param>
        /// <returns></returns>
        public async Task<IMember?> ReadAsync(long memberId)
        {
            return await _identityCache.GetAsync<Max.Identity.Domain.Member>($"{TAG_MEMBER_SECTION}{memberId}", true);
        }

        /// <summary>
        /// RemoveAsync
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public async Task RemoveAsync(long memberId)
        {
            //软删除
            this.Remove(memberId);

            //清除缓存
            await _identityCache.DeleteAsync($"{TAG_MEMBER_SECTION}{memberId}", true);
        }

        /// <summary>
        /// 刷新member缓存
        /// </summary>
        /// <param name="member"></param>
        /// <param name="expires"></param>
        /// <returns></returns>
        public async Task RefreshAsync(IMember member, DateTime expires)
        {
            if (member.Id > 0)
            {
                await _identityCache.SetAsync($"{TAG_MEMBER_SECTION}{member.Id}", member, expires, true);
            }
        }

        /// <summary>
        /// 刷新AccessChain
        /// </summary>
        /// <param name="oldToken"></param>
        /// <param name="accessChain"></param>
        /// <param name="expires"></param>
        /// <returns></returns>
        public async Task RefreshAccessChainAsync(string oldToken, IAccessChain accessChain, DateTime expires)
        {
            //清除旧AccessSession和User
            if (!string.IsNullOrWhiteSpace(oldToken))
            {
                await _identityCache.DeleteAsync($"{TAG_ACCESS_SECTION}{oldToken}", true);
            }

            //设置新缓存
            await _identityCache.SetAsync($"{TAG_ACCESS_SECTION}{accessChain!.AccessSession!.Token}", accessChain.AccessSession, expires, true);
            if (accessChain.AccessSession.MemberId > 0)
            {
                await RefreshAsync(accessChain.Member!, expires);
            }
        }
    }
}