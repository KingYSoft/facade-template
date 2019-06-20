using Abp.Modules;
using Abp.Reflection.Extensions;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.Oracle
{
    [DependsOn(
           typeof(FacadeProjectNameDomainServiceShareModule)
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
