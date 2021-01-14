using Abp.Dependency;
using Abp.Domain.Services;

namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    /// <summary>
    /// 领域服务和Application Services 是不同的，Application Services 返回的是DTO，而领域服务返回的是领域对象(实体或者值类型)。 
    /// 领域服务可以被应用服务和其它的领域服务调用，但是不可以被表现层直接调用(表现层可以直接调用应用服务)。
    /// </summary>
    public interface IFacadeProjectNameDomainServiceBase : IDomainService, ITransientDependency
    {
    }
}
