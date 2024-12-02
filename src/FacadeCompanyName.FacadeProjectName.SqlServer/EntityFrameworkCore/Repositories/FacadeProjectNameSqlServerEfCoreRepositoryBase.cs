using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace FacadeCompanyName.FacadeProjectName.SqlServer.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// EF Core Repository base
    /// efcore 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public class FacadeProjectNameSqlServerEfCoreRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<FacadeProjectNameSqlServerDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected FacadeProjectNameSqlServerEfCoreRepositoryBase(IDbContextProvider<FacadeProjectNameSqlServerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Add your common methods for all repositories
    }
}
