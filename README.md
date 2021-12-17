# Max
code max

## 仓储设施
四种仓储使用方式：
```cs
public SampleService(IUnitOfWork<MaxContext> unitOfWork,IRepository<XappSns> repository,IMaxIdentityRepository<XappSns> repo,ISampleRepository rep)
{
  _unitOfWork = unitOfWork;
  //_unitOfWork.GetRepository<XappSns>();
  _repository = repository;
  _repo = repo;
  _rep = rep;
}

//1. 注入 IUnitOfWork<MaxContext> unitOfWork 或 IUnitOfWork unitOfWork


//2. 注入 IRepository<XappSns> repository
  
  
//3. 注入范型实现 IMaxIdentityRepository<XappSns>

public interface IMaxIdentityRepository<T> : IRepository<T> where T : Entity{}

public class MaxIdentityRepository<T> : EfRepository<T>, IMaxIdentityRepository<T> where T : Entity
{
    public MaxIdentityRepository(MaxContext context) : base(context)
    {
    }
}


//4. 注入具体实现 ISampleRepository

public interface ISampleRepository : IRepository<XappSns>{}

public class SampleRepository : EfRepository<XappSns>, ISampleRepository
{
    public SampleRepository(MaxContext context) : base(context)
    {
    }
}
```

