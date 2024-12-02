using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore;

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
            Configuration.Modules.AbpEfCore().AddDbContext<FacadeProjectNameOracleDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    FacadeProjectNameOracleDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    FacadeProjectNameOracleDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameOracleModule).GetAssembly());
        }

    }
}
