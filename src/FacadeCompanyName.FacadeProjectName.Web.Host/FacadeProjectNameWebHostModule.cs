﻿using Abp.IO;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Facade.AspNetCore;
using Facade.AspNetCore.Configuration;
using Facade.AspNetCore.SignalR;
using Facade.AspNetCore.Zero;
using Facade.Core.Configuration;
using FacadeCompanyName.FacadeProjectName.Application;
using FacadeCompanyName.FacadeProjectName.DomainService.Folders;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.Web.Host.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;

namespace FacadeCompanyName.FacadeProjectName.Web.Host
{
    [DependsOn(
              typeof(FacadeProjectNameApplicationModule),
              typeof(FacadeAspNetCoreModule),
              typeof(FacadeAspNetCoreSignalRModule),
              typeof(FacadeAspNetCoreZeroModule)
              )]
    public class FacadeProjectNameWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public FacadeProjectNameWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.BuildConfiguration(new FacadeConfigurationBuilderOptions()
            {
                UserSecretsAssembly = typeof(FacadeProjectNameWebHostModule).GetAssembly()
            });
        }
        /// <summary>
        /// 预加载
        ///<see cref="DomainService.Share.Jobs.BackJob"/>
        /// </summary>
        public override void PreInitialize()
        {

            IocManager.Register<IFacadeConfiguration, FacadeConfiguration>(Abp.Dependency.DependencyLifeStyle.Singleton);
            var facadeConfiguration = IocManager.Resolve<FacadeConfiguration>();
            _appConfiguration.GetSection("FacadeConfiguration").Bind(facadeConfiguration);

            Configuration.Auditing.IsEnabled = false;
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.MultiTenancy.IsEnabled = FacadeProjectNameConsts.MultiTenancyEnabled;
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(FacadeProjectNameConsts.ConnectionStringName);

            ConfigureTokenAuth();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameWebHostModule).GetAssembly());
        }
        public override void PostInitialize()
        {
            SetAppFolders();
            // StartQuartz();
        }
        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(30);
        }
        private void SetAppFolders()
        {
            var appFolders = IocManager.Resolve<AppFolders>();

            appFolders.FileUploadFolder = Path.Combine(_env.WebRootPath, "uploads");
            appFolders.TempFileUploadFolder = Path.Combine(_env.WebRootPath, "uploads", "temps");
            appFolders.TempFileDownloadFolder = Path.Combine(_env.WebRootPath, "downloads", "temps");

            try
            {
                DirectoryHelper.CreateIfNotExists(appFolders.FileUploadFolder);
                DirectoryHelper.CreateIfNotExists(appFolders.TempFileUploadFolder);
                DirectoryHelper.CreateIfNotExists(appFolders.TempFileDownloadFolder);
            }
            catch { }
        }
    }
}

