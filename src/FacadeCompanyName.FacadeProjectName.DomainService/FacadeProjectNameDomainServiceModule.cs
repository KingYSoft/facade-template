using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Facade.Castle.NLogger;
using FacadeCompanyName.FacadeProjectName.DomainService.Localization;
using FacadeCompanyName.FacadeProjectName.Oracle;

namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    [DependsOn(
        typeof(FacadeCastleNLoggerModule),
        typeof(FacadeProjectNameOracleModule),
        typeof(AbpAutoMapperModule)
           )]
    public class FacadeProjectNameDomainServiceModule : AbpModule
    {

        public FacadeProjectNameDomainServiceModule()
        {
        }
        public override void PreInitialize()
        {
            FacadeProjectNameLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameDomainServiceModule).GetAssembly());
        }

    }
}
