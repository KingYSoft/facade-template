using Facade.Dapper;
using Facade.Dapper.Oracle;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.App;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore.Repositories
{
    public class AppOracleRepository : OracleQueryRepository<FacadeProjectNameOracleDbContext>, IAppOracleRepository
    {
        public AppOracleRepository(IFacadeConnectionProvider facadeConnectionProvider)
            : base(facadeConnectionProvider)
        {
        }
    }
}
