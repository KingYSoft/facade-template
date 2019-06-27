using Facade.Dapper;
using Facade.Dapper.Oracle;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.Demo;

namespace FacadeCompanyName.FacadeProjectName.Oracle
{
    public class DemoRepository : OracleDapperRepository<Demo, long>, IDemoRepository
    {
        public DemoRepository(IFacadeConnectionProvider facadeConnectionProvider)
            : base(facadeConnectionProvider)
        {
        }
    }
}
