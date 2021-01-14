using Abp.Dependency;
using Facade.Dapper.Oracle;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    public interface IAppRepository : IOracleQueryRepository, ITransientDependency
    {
    }
}
