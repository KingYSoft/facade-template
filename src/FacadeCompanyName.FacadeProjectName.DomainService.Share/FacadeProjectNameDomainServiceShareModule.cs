using Abp.Modules;
using Abp.Reflection.Extensions;
using Facade.Dapper.Oracle;
using Facade.NLogger;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.Interceptors;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    [DependsOn(
           typeof(FacadeNLoggerModule),
           typeof(FacadeDapperOralceModule)
           )]
    public class FacadeProjectNameDomainServiceShareModule : AbpModule
    {

        public FacadeProjectNameDomainServiceShareModule()
        {
        }
        public override void PreInitialize()
        {
            //注册拦截器
            DapperInterceptorRegistrar.Initialize(IocManager.IocContainer.Kernel);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameDomainServiceShareModule).GetAssembly());
        }

    }
}