/*
using System;
using System.Threading.Tasks;

using iMaxSys.Max.Data;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Max.Collection;
using iMaxSys.Data.Models;
using iMaxSys.Identity.Data.Repositories;

namespace iMaxSys.Identity.Services
{
    //public interface ISampleService : IDependency
    //{
    //    Task<XappSns> GetAsync(long id);

    //    Task<IPagedList<XappSns>> GetListAsync();
    //}

    //public class SampleService : ISampleService
    //{
    //    readonly IRepository<XappSns> _repository;
    //    readonly IUnitOfWork _unitOfWork;
    //    readonly IMaxIdentityRepository<XappSns> _repo;
    //    readonly ISampleRepository _rep;

    //    public SampleService(IUnitOfWork unitOfWork, IRepository<XappSns> repository, IMaxIdentityRepository<XappSns> repo, ISampleRepository rep)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _repository = repository;
    //        _repo = repo;
    //        _rep = rep;
    //    }

    //    public async Task<XappSns> GetAsync(long id)
    //    {
    //        //var repo1 = _unitOfWork.GetRepository<XappSns>();
    //        //var sns1 = await repo1.Include(x => x.Xapp).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    //        ////============================================
    //        var repo2 = _unitOfWork.GetRepo<XappSns>();
    //        ISpecification<XappSns> spec = new Specification<XappSns>(x => x.Id == id);
    //        spec = spec.AddInclude(x => x.Xapp).ApplyPaging(0, 10);//.ApplyOrderBy(); (x => x.Id == id);
    //        //spec = spec.AddIncludes(x=>x.Include(y=>y.Xapp).ThenInclude(z=>z.XappSns));
    //        var sns2 = await repo2.FirstOrDefaultAsync(spec);

    //        //============================================
    //        var spec1 = new Specification<XappSns>(x => x.Id == id);
    //        spec1.ApplyTracking();
    //        var sns3 = await _repository.FirstOrDefaultAsync(spec1);
    //        sns3.Description = $"修改测试-{DateTime.Now}";
    //        await _repository.UpdateAsync(sns3);


    //        //============================================
    //        //var sns4 = await _repo.FirstOrDefaultAsync(spec);

    //        await _unitOfWork.SaveChangesAsync();

    //        return sns3;
    //    }

    //    public async Task<IPagedList<XappSns>> GetListAsync()
    //    {
    //        //var repo1 = _unitOfWork.GetRepository<XappSns>();
    //        //var list1 = await repo1.Include(x => x.Xapp).ToPagedListAsync(0, 5);

    //        //============================================
    //        var repo2 = _unitOfWork.GetRepo<XappSns>();
    //        ISpecification<XappSns> spec = new Specification<XappSns>(null);
    //        spec = spec.AddInclude(x => x.Xapp).ApplyPaging(0, 5);
    //        //spec = spec.AddIncludes(x=>x.Include(y=>y.Xapp).ThenInclude(z=>z.XappSns));
    //        var sns2 = await repo2.GetPagedListAsync(spec);

    //        //============================================
    //        var sns3 = await _repository.GetPagedListAsync(spec);
            
    //        //============================================
    //        var sns4 = await _repo.GetPagedListAsync(spec);

    //        return sns4;
    //    }
    //}
}
*/