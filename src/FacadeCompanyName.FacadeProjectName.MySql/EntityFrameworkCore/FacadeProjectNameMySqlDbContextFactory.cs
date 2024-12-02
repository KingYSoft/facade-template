using Facade.Core.Configuration;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FacadeCompanyName.FacadeProjectName.MySql.EntityFrameworkCore
{
    public class FacadeProjectNameMySqlDbContextFactory : IDesignTimeDbContextFactory<FacadeProjectNameMySqlDbContext>
    {
        public FacadeProjectNameMySqlDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FacadeProjectNameMySqlDbContext>();
            var configuration = ConfigurationHelper.BuildConfiguration(new FacadeConfigurationBuilderOptions()
            {
                BasePath = WebContentDirectoryFinder.CalculateContentRootFolder(),
                EnvironmentName = "Development"
            });
            System.Console.WriteLine(configuration.GetConnectionString(FacadeProjectNameConsts.MySqlConnectionStringName));

            FacadeProjectNameMySqlDbContextConfigurer.Configure(builder, configuration.GetConnectionString(FacadeProjectNameConsts.MySqlConnectionStringName));

            return new FacadeProjectNameMySqlDbContext(builder.Options);
        }
    }
}
