using Facade.Dapper.MySql;
using Microsoft.EntityFrameworkCore;

namespace FacadeCompanyName.FacadeProjectName.MySql.EntityFrameworkCore
{
    public class FacadeProjectNameMySqlDbContext : MySqlDbContext
    {
        // 配置 DbSet 自动注册 ef core IRepository 


        public FacadeProjectNameMySqlDbContext(DbContextOptions<FacadeProjectNameMySqlDbContext> options)
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
