using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FacadeCompanyName.FacadeProjectName.SqlServer.EntityFrameworkCore
{
    public class FacadeProjectNameSqlServerDbContext : AbpDbContext
    {
        // 配置 DbSet 自动注册 ef core IRepository 


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
