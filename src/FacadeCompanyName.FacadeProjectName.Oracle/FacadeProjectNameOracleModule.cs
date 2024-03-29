﻿using Abp.Modules;
using Abp.Reflection.Extensions;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.Oracle.Interceptors;

namespace FacadeCompanyName.FacadeProjectName.Oracle
{
    [DependsOn(
           typeof(FacadeProjectNameDomainServiceShareModule)
           )]
    public class FacadeProjectNameOracleModule : AbpModule
    {

        public FacadeProjectNameOracleModule()
        {
        }
        public override void PreInitialize()
        {
            //注册拦截器
            InterceptorRegistrar.Initialize(IocManager.IocContainer.Kernel);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameOracleModule).GetAssembly());
        }

    }
}
