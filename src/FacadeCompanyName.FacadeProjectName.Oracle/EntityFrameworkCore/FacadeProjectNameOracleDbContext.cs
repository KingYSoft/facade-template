using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore
{
    public class FacadeProjectNameOracleDbContext : AbpDbContext
    {
        // 配置 DbSet 自动注册 ef core IRepository 


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
