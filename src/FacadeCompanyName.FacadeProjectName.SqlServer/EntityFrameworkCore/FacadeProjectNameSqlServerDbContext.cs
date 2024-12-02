using Facade.Dapper.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace FacadeCompanyName.FacadeProjectName.SqlServer.EntityFrameworkCore
{
    public class FacadeProjectNameSqlServerDbContext : SqlServerDbContext
    {
        // 配置 DbSet 自动注册 ef core IRepotory 


        public FacadeProjectNameSqlServerDbContext(DbContextOptions<FacadeProjectNameSqlServerDbContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // table 配置

        }
    }
}
