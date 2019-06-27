using Abp.Dependency;
using Facade.Dapper.Oracle;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share.Demo
{
    public interface IDemoRepository : IOracleDapperRepository<Demo, long>, ITransientDependency
    {
    }
}
