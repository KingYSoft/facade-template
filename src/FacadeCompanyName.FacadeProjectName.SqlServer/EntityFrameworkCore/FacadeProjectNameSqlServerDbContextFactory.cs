using Facade.Core.Configuration;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FacadeCompanyName.FacadeProjectName.SqlServer.EntityFrameworkCore
{
    public class FacadeProjectNameSqlServerDbContextFactory : IDesignTimeDbContextFactory<FacadeProjectNameSqlServerDbContext>
    {
        public FacadeProjectNameSqlServerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FacadeProjectNameSqlServerDbContext>();
            var configuration = ConfigurationHelper.BuildConfiguration(new FacadeConfigurationBuilderOptions()
            {
                BasePath = WebContentDirectoryFinder.CalculateContentRootFolder(),
                EnvironmentName = "Development"
            });
            System.Console.WriteLine(configuration.GetConnectionString(FacadeProjectNameConsts.SqlServerConnectionStringName));

            FacadeProjectNameSqlServerDbContextConfigurer.Configure(builder, configuration.GetConnectionString(FacadeProjectNameConsts.SqlServerConnectionStringName));

            return new FacadeProjectNameSqlServerDbContext(builder.Options);
        }
    }
}
