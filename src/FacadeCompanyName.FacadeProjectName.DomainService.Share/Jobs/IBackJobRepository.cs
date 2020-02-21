using Abp.Dependency;
using Facade.Dapper.Oracle;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share.Jobs
{
    public interface IBackJobRepository : IOracleDapperRepository<BackJob, long>, ITransientDependency
    {
    }
}
