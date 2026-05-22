using Facade.Dapper;
using Facade.Dapper.SqlServer;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.App;

namespace FacadeCompanyName.FacadeProjectName.SqlServer.EntityFrameworkCore.Repositories
{
    public class AppSqlServerRepository : SqlServerQueryRepository<FacadeProjectNameSqlServerDbContext>, IAppSqlServerRepository
    {
        public AppSqlServerRepository(IFacadeConnectionProvider facadeConnectionProvider)
            : base(facadeConnectionProvider)
        {
        }
    }
}
