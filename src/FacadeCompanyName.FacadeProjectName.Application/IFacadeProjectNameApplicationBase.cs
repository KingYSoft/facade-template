using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Facade.Core.Application.Services;

namespace FacadeCompanyName.FacadeProjectName.Application
{
    public interface IFacadeProjectNameApplicationBase : IApplicationService, ITransientDependency
    {
    }

    public interface IFacadeProjectNameApplicationBase<TEntityDto>
     : IFacadeProjectNameApplicationBase<TEntityDto, int>
     where TEntityDto : IEntityDto<int>
    {

    }
    public interface IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey>
       : IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, PagedAndSortedResultRequestDto>
       where TEntityDto : IEntityDto<TPrimaryKey>
    {

    }
    public interface IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, in TGetAllInput>
      : IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, TGetAllInput, TEntityDto, TEntityDto>
      where TEntityDto : IEntityDto<TPrimaryKey>
    {

    }
    public interface IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput>
       : IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TCreateInput>
       where TEntityDto : IEntityDto<TPrimaryKey>
       where TCreateInput : IEntityDto<TPrimaryKey>
    {

    }
    public interface IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput>
       : IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>>
       where TEntityDto : IEntityDto<TPrimaryKey>
       where TUpdateInput : IEntityDto<TPrimaryKey>
    {

    }
    public interface IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput, in TGetInput>
      : IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, EntityDto<TPrimaryKey>>
      where TEntityDto : IEntityDto<TPrimaryKey>
      where TUpdateInput : IEntityDto<TPrimaryKey>
      where TGetInput : IEntityDto<TPrimaryKey>
    {
    }
    public interface IFacadeProjectNameApplicationBase<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput, in TGetInput, in TDeleteInput>
        : IAsyncDapperCrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>, ITransientDependency
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
        where TDeleteInput : IEntityDto<TPrimaryKey>
    {
    }

}