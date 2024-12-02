using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using FacadeCompanyName.FacadeProjectName.MySql.EntityFrameworkCore;
using FacadeCompanyName.FacadeProjectName.SqlServer.EntityFrameworkCore;
using System;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    public class MyConnectionStringResolver : DefaultConnectionStringResolver
    {
        private readonly IFacadeConfiguration _facadeConfiguration;

        public MyConnectionStringResolver(IAbpStartupConfiguration configuration, IFacadeConfiguration facadeConfiguration)
            : base(configuration)
        {
            _facadeConfiguration = facadeConfiguration;
        }

        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            if (args["DbContextConcreteType"] as Type == typeof(FacadeProjectNameSqlServerDbContext))
            {
                return _facadeConfiguration.SqlServerConnString;
            }
            else if (args["DbContextConcreteType"] as Type == typeof(FacadeProjectNameMySqlDbContext))
            {
                return _facadeConfiguration.MySqlConnString;
            }

            return base.GetNameOrConnectionString(args);
        }
    }
}
