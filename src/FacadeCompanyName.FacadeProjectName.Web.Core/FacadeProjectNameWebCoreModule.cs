using Abp.Dependency;
using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Facade.AspNetCore;
using Facade.AspNetCore.Configuration;
using Facade.AspNetCore.Zero;
using Facade.Core.Configuration;
using FacadeCompanyName.FacadeProjectName.Application;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.Web.Core.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace FacadeCompanyName.FacadeProjectName.Web.Core
{
    [DependsOn(
                typeof(FacadeProjectNameApplicationModule),
                typeof(FacadeAspNetCoreModule),
                typeof(FacadeAspNetCoreZeroModule)
                )]
    public class FacadeProjectNameWebCoreModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public FacadeProjectNameWebCoreModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.BuildConfiguration(new FacadeConfigurationBuilderOptions()
            {
                UserSecretsAssembly = typeof(FacadeProjectNameWebCoreModule).GetAssembly()
            });
        }

        public override void PreInitialize()
        {
            IocManager.RegisterIfNot<IFacadeConfiguration, FacadeConfiguration>(Abp.Dependency.DependencyLifeStyle.Singleton);
            var facadeConfiguration = IocManager.Resolve<FacadeConfiguration>();
            _appConfiguration.GetSection("FacadeConfiguration").Bind(facadeConfiguration);
            facadeConfiguration.AppRootPath = _env.ContentRootPath;
            facadeConfiguration.IsDevelopment = _env.EnvironmentName.Equals("Development", StringComparison.OrdinalIgnoreCase);
            facadeConfiguration.AppEnvName = _env.EnvironmentName;
            facadeConfiguration.SqlServerConnString = _appConfiguration.GetConnectionString(FacadeProjectNameConsts.SqlServerConnectionStringName);
            facadeConfiguration.MySqlConnString = _appConfiguration.GetConnectionString(FacadeProjectNameConsts.MySqlConnectionStringName);

            Configuration.Auditing.IsEnabled = false;
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;
            Configuration.MultiTenancy.IsEnabled = FacadeProjectNameConsts.MultiTenancyEnabled;
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(FacadeProjectNameConsts.ConnectionStringName);
            Configuration.UnitOfWork.Timeout = new TimeSpan(0, 0, 30);

            Configuration.Localization.Languages.Clear();
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", isDefault: true));
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-Hans", "中文简体"));
            ConfigureTokenAuth();

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameWebCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
        }

        public override void Shutdown()
        {
        }

        private void ConfigureTokenAuth()
        {
            IocManager.RegisterIfNot<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);

        }

    }
}
