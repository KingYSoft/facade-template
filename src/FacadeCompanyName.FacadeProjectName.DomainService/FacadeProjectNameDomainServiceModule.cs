using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Facade.AutoMapper;
using Facade.Quartz;
using FacadeCompanyName.FacadeProjectName.DomainService.Localization;
using FacadeCompanyName.FacadeProjectName.Oracle;

namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    [DependsOn(
        typeof(FacadeProjectNameOracleModule),
        typeof(FacadeQuartzModule),
        typeof(FacadeAutoMapperModule)
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
            var thisAssembly = typeof(FacadeProjectNameDomainServiceModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                //cfg => cfg.AddProfiles(thisAssembly)
                cfg => cfg.AddMaps(thisAssembly)
            );
        }

    }
}
