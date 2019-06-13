using Abp.Modules;
using Abp.Reflection.Extensions;
using Facade.Dapper.Oracle;

namespace FacadeCompanyName.FacadeProjectName.Oracle
{
    [DependsOn(
           typeof(FacadeDapperOralceModule)
           )]
    public class FacadeProjectNameOracleModule : AbpModule
    {

        public FacadeProjectNameOracleModule()
        {
        }
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameOracleModule).GetAssembly());
        }

    }
}
