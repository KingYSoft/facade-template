﻿using Abp.AspNetCore.SignalR.Notifications;
using Abp.IO;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Facade.AspNetCore.Configuration;
using Facade.Core.Configuration;
using FacadeCompanyName.FacadeProjectName.DomainService.Folders;
using FacadeCompanyName.FacadeProjectName.Web.Core;
using FacadeCompanyName.FacadeProjectName.Web.Host.Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FacadeCompanyName.FacadeProjectName.Web.Host
{
    [DependsOn(
              typeof(FacadeProjectNameWebCoreModule)
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
        /// </summary>
        public override void PreInitialize()
        {
            // set licence
            Facade.Configuration.FacadeCoreConfigurationExtensions.Facade(Configuration.Modules).Configure(options =>
            {
                options.Licence = "";
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameWebHostModule).GetAssembly());
            //hub 被继承后，HubconnectionStore 连接数是空
            if (Configuration.Notifications.Notifiers.Contains(typeof(SignalRRealTimeNotifier)))
            {
                Configuration.Notifications.Notifiers.Remove(typeof(SignalRRealTimeNotifier));
            }
            Configuration.Notifications.Notifiers.Add<NewSignalRRealTimeNotifier>();
        }
        public override void PostInitialize()
        {
            // StartQuartz();

            SetAppFolders();
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

