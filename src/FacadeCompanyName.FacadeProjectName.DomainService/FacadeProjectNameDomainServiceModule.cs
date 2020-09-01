using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FacadeCompanyName.FacadeProjectName.DomainService.Interceptors;
using FacadeCompanyName.FacadeProjectName.DomainService.Localization;
using FacadeCompanyName.FacadeProjectName.Oracle;

namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    [DependsOn(
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
            //注册拦截器
            InterceptorRegistrar.Initialize(IocManager.IocContainer.Kernel);
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
