using Abp.Dependency;
using Facade.Dapper.Oracle;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share.App
{
    public interface IAppOracleRepository : IOracleQueryRepository, ITransientDependency
    {
    }
}
