using Abp.Dependency;
using Facade.Dapper.MySql;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share.App
{
    public interface IAppMySqlRepository : IMySqlQueryRepository, ITransientDependency
    {
    }
}
