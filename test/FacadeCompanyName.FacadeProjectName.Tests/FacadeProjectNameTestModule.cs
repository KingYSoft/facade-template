using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Localization;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Reflection.Extensions;
using Abp.TestBase;
using Castle.Facilities.Logging;
using Facade.NLogger;
using FacadeCompanyName.FacadeProjectName.Application;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace FacadeCompanyName.FacadeProjectName.Tests
{
    [DependsOn(
           typeof(AbpTestBaseModule),
           typeof(FacadeProjectNameApplicationModule)
           )]
    public class FacadeProjectNameTestModule : AbpModule
    { 
        public FacadeProjectNameTestModule()
        { 
        }

        public override void PreInitialize()
        {
            IocManager.IocContainer.AddFacility<LoggingFacility>(f =>
            {
                f.UseFacadeNLog($"{Directory.GetCurrentDirectory()}\\NLog.config");
            });
            IocManager.Register<IFacadeConfiguration, FacadeConfiguration>(Abp.Dependency.DependencyLifeStyle.Singleton);
            var facadeConfiguration = IocManager.Resolve<FacadeConfiguration>();
            facadeConfiguration.AppName = "FacadeProjectName_Tests";
            facadeConfiguration.AppRootPath = Directory.GetCurrentDirectory();
            facadeConfiguration.IsDevelopment = true;
            facadeConfiguration.AppEnvName = "Development";

            // Disable static mapper usage since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052)
            // Configuration.Modules.AbpAutoMapper().UseStaticMapper = false;

            Configuration.Auditing.IsEnabled = false;
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.MultiTenancy.IsEnabled = FacadeProjectNameConsts.MultiTenancyEnabled;
            Configuration.DefaultNameOrConnectionString = "Data Source=ORCL;Persist Security Info=True;User Id=ORCL;Password=ORCL;";

            Configuration.Localization.Languages.Clear();
            Configuration.Localization.Languages.Add(new LanguageInfo("zh", "ÖÐÎÄ¼òÌå", isDefault: true));
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));

            Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(FacadeProjectNameTestModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);
            //  ServiceCollectionRegistrar.Register(IocManager);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                //cfg => cfg.AddProfiles(thisAssembly)
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
