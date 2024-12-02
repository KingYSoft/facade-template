using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace FacadeCompanyName.FacadeProjectName.MySql.EntityFrameworkCore
{
    public static class FacadeProjectNameMySqlDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FacadeProjectNameMySqlDbContext> builder, string connectionString)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 18));
            builder.UseMySql(connectionString, serverVersion);
        }

        public static void Configure(DbContextOptionsBuilder<FacadeProjectNameMySqlDbContext> builder, DbConnection connection)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 18));
            builder.UseMySql(connection, serverVersion);
        }
    }
}
