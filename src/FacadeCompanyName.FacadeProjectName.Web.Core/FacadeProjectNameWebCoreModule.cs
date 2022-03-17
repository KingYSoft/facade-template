﻿using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Services;
using Abp.IO;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using Facade.AspNetCore;
using Facade.AspNetCore.Configuration;
using Facade.AspNetCore.SignalR;
using Facade.AspNetCore.Zero;
using Facade.Core.Configuration;
using FacadeCompanyName.FacadeProjectName.Application;
using FacadeCompanyName.FacadeProjectName.DomainService.Folders;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.Web.Core.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System;
using System.IO;
using System.Text;

namespace FacadeCompanyName.FacadeProjectName.Web.Core
{
    [DependsOn(
                typeof(FacadeProjectNameApplicationModule),
                typeof(FacadeAspNetCoreModule),
                typeof(FacadeAspNetCoreSignalRModule),
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
            Configuration.UnitOfWork.ConventionalUowSelectors.Add(t =>
                typeof(IDomainService).IsAssignableFrom(t) ||
                typeof(IBackgroundJob<>).IsAssignableFrom(t) ||
                typeof(IBackgroundWorker).IsAssignableFrom(t) ||
                typeof(IJob).IsAssignableFrom(t) ||
                typeof(IBackgroundJobStore).IsAssignableFrom(t));


            IocManager.RegisterIfNot<IFacadeConfiguration, FacadeConfiguration>(Abp.Dependency.DependencyLifeStyle.Singleton);
            var facadeConfiguration = IocManager.Resolve<FacadeConfiguration>();
            _appConfiguration.GetSection("FacadeConfiguration").Bind(facadeConfiguration);
            facadeConfiguration.Redis_Host = _appConfiguration["Redis:Host"] ?? "localhost";
            facadeConfiguration.Redis_Port = int.Parse(_appConfiguration["Redis:Port"] ?? "6379");

            Configuration.Auditing.IsEnabled = false;
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;
            Configuration.MultiTenancy.IsEnabled = FacadeProjectNameConsts.MultiTenancyEnabled;
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(FacadeProjectNameConsts.ConnectionStringName);
            Configuration.UnitOfWork.Timeout = new TimeSpan(0, 0, 30);

            ConfigureTokenAuth();

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameWebCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            SetAppFolders();
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

        private void SetAppFolders()
        {
            var appFolders = IocManager.Resolve<AppFolders>();


            appFolders.FileUploadFolder = Path.Combine(_env.WebRootPath, "files", "uploads");
            appFolders.TempFileUploadFolder = Path.Combine(_env.WebRootPath, "temps", "uploads");
            appFolders.TempFileDownloadFolder = Path.Combine(_env.WebRootPath, "temps", "downloads");

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
