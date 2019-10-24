using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FacadeCompanyName.FacadeProjectName.DomainService;

namespace FacadeCompanyName.FacadeProjectName.Application
{
    [DependsOn(
        typeof(FacadeProjectNameDomainServiceModule)
           )]
    public class FacadeProjectNameApplicationModule : AbpModule
    {

        public FacadeProjectNameApplicationModule()
        {
        }
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(FacadeProjectNameApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }

    }
}
