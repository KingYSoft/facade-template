using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore
{
    public static class FacadeProjectNameOracleDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FacadeProjectNameOracleDbContext> builder, string connectionString)
        {
            builder.UseOracle(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<FacadeProjectNameOracleDbContext> builder, DbConnection connection)
        {
            builder.UseOracle(connection);
        }
    }
}
