using Facade.Dapper;
using Facade.Dapper.Oracle;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore.Repositories
{
    public class AppQueryRepository : OracleQueryRepository, IAppQueryRepository
    {
        public AppQueryRepository(IFacadeConnectionProvider facadeConnectionProvider)
            : base(facadeConnectionProvider)
        {
        }
    }
}
