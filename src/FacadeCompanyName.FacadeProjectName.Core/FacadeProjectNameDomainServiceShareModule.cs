using Abp.Modules;
using Abp.Reflection.Extensions;
using Facade.Dapper.Oracle;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    [DependsOn(
           typeof(FacadeDapperOralceModule)
           )]
    public class FacadeProjectNameDomainServiceShareModule : AbpModule
    {

        public FacadeProjectNameDomainServiceShareModule()
        {
        }
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameDomainServiceShareModule).GetAssembly());
        }

    }
}