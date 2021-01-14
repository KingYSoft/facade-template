using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Facade.Core.Application.Services;
using Facade.Core.Domain.Repositories;
using Facade.Core.Web;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.Application
{
    public abstract class FacadeProjectNameApplicationBase : ApplicationService, IFacadeProjectNameApplicationBase
    {
        private readonly IHttpRequestHeaderParser _httpRequestHeaderParser;
        protected FacadeProjectNameApplicationBase(IHttpRequestHeaderParser httpRequestHeaderParser)
            : base()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
            _httpRequestHeaderParser = httpRequestHeaderParser;
        }
    }
    public abstract class FacadeProjectNameApplicationBase<TEntity, TEntityDto>
       : FacadeProjectNameApplicationBase<TEntity, TEntityDto, int>
       where TEntity : class, IEntity<int>
       where TEntityDto : IEntityDto<int>
    {
        protected FacadeProjectNameApplicationBase(IFacadeDapperRepository<TEntity, int> repository, IHttpRequestHeaderParser httpRequestHeaderParser) : base(repository, httpRequestHeaderParser)
        {
        }

    }
    public abstract class FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey>
       : FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey, PagedAndSortedResultRequestDto>
       where TEntity : class, IEntity<TPrimaryKey>
       where TEntityDto : IEntityDto<TPrimaryKey>
    {
        protected FacadeProjectNameApplicationBase(IFacadeDapperRepository<TEntity, TPrimaryKey> repository, IHttpRequestHeaderParser httpRequestHeaderParser) : base(repository, httpRequestHeaderParser)
        {
        }

    }
    public abstract class FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput>
      : FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TEntityDto, TEntityDto>
      where TEntity : class, IEntity<TPrimaryKey>
      where TEntityDto : IEntityDto<TPrimaryKey>
    {
        protected FacadeProjectNameApplicationBase(IFacadeDapperRepository<TEntity, TPrimaryKey> repository, IHttpRequestHeaderParser httpRequestHeaderParser) : base(repository, httpRequestHeaderParser)
        {
        }

    }
    public abstract class FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput>
       : FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TCreateInput>
      where TEntity : class, IEntity<TPrimaryKey>
       where TEntityDto : IEntityDto<TPrimaryKey>
       where TCreateInput : IEntityDto<TPrimaryKey>
    {
        protected FacadeProjectNameApplicationBase(IFacadeDapperRepository<TEntity, TPrimaryKey> repository, IHttpRequestHeaderParser httpRequestHeaderParser) : base(repository, httpRequestHeaderParser)
        {
        }

    }
    public abstract class FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
       : FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>>
      where TEntity : class, IEntity<TPrimaryKey>
       where TEntityDto : IEntityDto<TPrimaryKey>
       where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        protected FacadeProjectNameApplicationBase(IFacadeDapperRepository<TEntity, TPrimaryKey> repository, IHttpRequestHeaderParser httpRequestHeaderParser) : base(repository, httpRequestHeaderParser)
        {
        }
    }
    public abstract class FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput>
      : FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, EntityDto<TPrimaryKey>>
      where TEntity : class, IEntity<TPrimaryKey>
      where TEntityDto : IEntityDto<TPrimaryKey>
      where TUpdateInput : IEntityDto<TPrimaryKey>
      where TGetInput : IEntityDto<TPrimaryKey>
    {
        protected FacadeProjectNameApplicationBase(IFacadeDapperRepository<TEntity, TPrimaryKey> repository, IHttpRequestHeaderParser httpRequestHeaderParser)
             : base(repository, httpRequestHeaderParser)
        {

        }
    }

    public abstract class FacadeProjectNameApplicationBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
        : AsyncDapperCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>,
        IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
        where TDeleteInput : IEntityDto<TPrimaryKey>
    {
        private readonly IHttpRequestHeaderParser _httpRequestHeaderParser;
        protected FacadeProjectNameApplicationBase(IFacadeDapperRepository<TEntity, TPrimaryKey> repository, IHttpRequestHeaderParser httpRequestHeaderParser)
           : base(repository)
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
            _httpRequestHeaderParser = httpRequestHeaderParser;
        }
    }
}


