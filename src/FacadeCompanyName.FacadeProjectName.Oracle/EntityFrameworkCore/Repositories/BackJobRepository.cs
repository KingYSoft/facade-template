using Facade.Dapper;
using Facade.Dapper.Oracle;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.Jobs;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore.Repositories
{
    public class BackJobRepository : OracleDapperRepository<BackJob, long>, IBackJobRepository
    {
        public BackJobRepository(IFacadeConnectionProvider facadeConnectionProvider)
            : base(facadeConnectionProvider)
        {
        }
    }
}