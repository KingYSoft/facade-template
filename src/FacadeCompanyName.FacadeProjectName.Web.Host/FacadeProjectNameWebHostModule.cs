using Abp.IO;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Facade.AspNetCore.Configuration;
using Facade.Core.Configuration;
using FacadeCompanyName.FacadeProjectName.Web.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

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
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameWebHostModule).GetAssembly());
        }
        public override void PostInitialize()
        { 
            // StartQuartz();
        } 
    }
}

