using Abp.Modules;
using Abp.Reflection.Extensions;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using System.Collections.Generic;
using System.Reflection;

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
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameOracleModule).GetAssembly());
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(FacadeProjectNameOracleModule).GetAssembly() });
        }

    }
}
