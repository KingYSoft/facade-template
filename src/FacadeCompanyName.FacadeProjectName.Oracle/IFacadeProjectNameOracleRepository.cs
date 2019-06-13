using Abp.Dependency;
using Facade.Dapper.Oracle;

namespace FacadeCompanyName.FacadeProjectName.Oracle
{
    public interface IFacadeProjectNameOracleRepository : IOracleDapperRepository, ITransientDependency
    {
    }
}
