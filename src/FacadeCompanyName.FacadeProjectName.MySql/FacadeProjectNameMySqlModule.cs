using Abp.Dependency;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.MySql.EntityFrameworkCore;

namespace FacadeCompanyName.FacadeProjectName.MySql
{
    [DependsOn(
           typeof(FacadeProjectNameDomainServiceShareModule)
           )]
    public class FacadeProjectNameMySqlModule : AbpModule
    { 
        public FacadeProjectNameMySqlModule()
        {
        }
        public override void PreInitialize()
        {
            Configuration.Modules.AbpEfCore().AddDbContext<FacadeProjectNameMySqlDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    FacadeProjectNameMySqlDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    FacadeProjectNameMySqlDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameMySqlModule).GetAssembly());
        }

    }
}
