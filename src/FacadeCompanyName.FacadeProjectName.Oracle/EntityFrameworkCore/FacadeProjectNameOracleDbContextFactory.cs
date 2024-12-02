using Facade.Core.Configuration;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore
{
    public class FacadeProjectNameOracleDbContextFactory : IDesignTimeDbContextFactory<FacadeProjectNameOracleDbContext>
    {
        public FacadeProjectNameOracleDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FacadeProjectNameOracleDbContext>();
            var configuration = ConfigurationHelper.BuildConfiguration(new FacadeConfigurationBuilderOptions()
            {
                BasePath = WebContentDirectoryFinder.CalculateContentRootFolder(),
                EnvironmentName = "Development"
            });
            System.Console.WriteLine(configuration.GetConnectionString(FacadeProjectNameConsts.ConnectionStringName));

            FacadeProjectNameOracleDbContextConfigurer.Configure(builder, configuration.GetConnectionString(FacadeProjectNameConsts.ConnectionStringName));

            return new FacadeProjectNameOracleDbContext(builder.Options);
        }
    }
}
