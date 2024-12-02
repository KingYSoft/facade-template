using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.SqlServer.EntityFrameworkCore;

namespace FacadeCompanyName.FacadeProjectName.SqlServer
{
    [DependsOn(
           typeof(FacadeProjectNameDomainServiceShareModule)
           )]
    public class FacadeProjectNameSqlServerModule : AbpModule
    {
        public FacadeProjectNameSqlServerModule()
        {
        }
        public override void PreInitialize()
        {
            Configuration.Modules.AbpEfCore().AddDbContext<FacadeProjectNameSqlServerDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    FacadeProjectNameSqlServerDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    FacadeProjectNameSqlServerDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameSqlServerModule).GetAssembly());
        }

    }
}
