using Abp.Dapper.Repositories;
using Facade.Dapper.Oracle;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share.Demo
{
    public interface IDemoRepository : IDapperRepository<Demo, long>, IOracleDapperRepository
    {
    }
}
