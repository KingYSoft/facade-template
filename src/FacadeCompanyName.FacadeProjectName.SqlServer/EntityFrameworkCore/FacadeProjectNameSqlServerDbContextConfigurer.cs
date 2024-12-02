using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FacadeCompanyName.FacadeProjectName.SqlServer.EntityFrameworkCore
{
    public class FacadeProjectNameSqlServerDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FacadeProjectNameSqlServerDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<FacadeProjectNameSqlServerDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
