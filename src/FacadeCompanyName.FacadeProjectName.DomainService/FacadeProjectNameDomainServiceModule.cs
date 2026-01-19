using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MailKit;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using Facade.AutoMapper;
using Facade.Quartz;
using FacadeCompanyName.FacadeProjectName.DomainService.BackgroundWorkers;
using FacadeCompanyName.FacadeProjectName.DomainService.Features;
using FacadeCompanyName.FacadeProjectName.DomainService.Localization;
using FacadeCompanyName.FacadeProjectName.DomainService.Navigation;
using FacadeCompanyName.FacadeProjectName.DomainService.SettingDefinitions;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.MySql;
using FacadeCompanyName.FacadeProjectName.Oracle;
using FacadeCompanyName.FacadeProjectName.SqlServer;

namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    [DependsOn(
        typeof(FacadeProjectNameOracleModule),
        typeof(FacadeProjectNameSqlServerModule),
        typeof(FacadeProjectNameMySqlModule),
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
            Configuration.ReplaceService<IConnectionStringResolver, MyConnectionStringResolver>();

            FacadeProjectNameLocalizationConfigurer.Configure(Configuration.Localization);

            //Configuration.Authorization.Providers.Add<MyAuthorizationProvider>();
            Configuration.Navigation.Providers.Add<MyNavigationProvider>();
            Configuration.Features.Providers.Add<MyFeatureProvider>();

            Configuration.Settings.Providers.Add<MyEmailSettingProvider>();
            Configuration.Settings.Providers.Add<MyLocalizationSettingProvider>();

            Configuration.ReplaceService<IMailKitSmtpBuilder, MyMailKitSmtpBuilder>(DependencyLifeStyle.Transient);
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

        public override void PostInitialize()
        {
            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                //Worker DI.
                IocManager.Resolve<IBackgroundWorkerManager>().Add(IocManager.Resolve<ClearLoggerWorker>());
            }
        }
    }
}
