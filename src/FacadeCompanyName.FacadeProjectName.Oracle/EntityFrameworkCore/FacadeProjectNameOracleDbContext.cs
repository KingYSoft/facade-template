using Facade.Dapper.Oracle;
using Microsoft.EntityFrameworkCore;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore
{
    public class FacadeProjectNameOracleDbContext : OracleDbContext
    {
        // 配置 DbSet 自动注册 ef core IRepotory 


        public FacadeProjectNameOracleDbContext(DbContextOptions<FacadeProjectNameOracleDbContext> options)
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
