using Abp.Dependency;
using Facade.Dapper.SqlServer;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share.App
{
    public interface IAppSqlServerRepository : ISqlServerQueryRepository, ITransientDependency
    {
    }
}
