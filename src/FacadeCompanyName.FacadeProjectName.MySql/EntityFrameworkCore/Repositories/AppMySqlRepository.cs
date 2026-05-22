using Facade.Dapper;
using Facade.Dapper.MySql;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.App;

namespace FacadeCompanyName.FacadeProjectName.MySql.EntityFrameworkCore.Repositories
{
    public class AppMySqlRepository : MySqlQueryRepository<FacadeProjectNameMySqlDbContext>, IAppMySqlRepository
    {
        public AppMySqlRepository(IFacadeConnectionProvider facadeConnectionProvider)
           : base(facadeConnectionProvider)
        {
        }
    }
}
