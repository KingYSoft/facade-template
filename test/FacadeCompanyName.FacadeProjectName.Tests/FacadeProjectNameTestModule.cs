using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Localization;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Reflection.Extensions;
using FacadeCompanyName.FacadeProjectName.Application;
using FacadeCompanyName.FacadeProjectName.DomainService;

namespace FacadeCompanyName.FacadeProjectName.Tests
{
    [DependsOn(
           typeof(FacadeProjectNameApplicationModule)
           )]
    public class FacadeProjectNameTestModule : AbpModule
    {
        public FacadeProjectNameTestModule()
        {
        }

        public override void PreInitialize()
        {
            // Disable static mapper usage since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052)
            Configuration.Modules.AbpAutoMapper().UseStaticMapper = false;

            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.MultiTenancy.IsEnabled = FacadeProjectNameConsts.MultiTenancyEnabled;
            Configuration.DefaultNameOrConnectionString = "Data Source=ORCL;Persist Security Info=True;User Id=ORCL;Password=ORCL;";
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "ÖÐÎÄ¼òÌå", isDefault: true));
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));

            Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameTestModule).GetAssembly());
            //  ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
