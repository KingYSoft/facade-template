using Abp.Data;
using Facade.Dapper.Oracle;

namespace FacadeCompanyName.FacadeProjectName.Oracle
{
    public class FacadeProjectNameOracleRepository : OracleDapperRepository, IFacadeProjectNameOracleRepository
    {
        public FacadeProjectNameOracleRepository(IActiveTransactionProvider activeTransactionProvider)
            : base(activeTransactionProvider)
        {
        }
    }
}
