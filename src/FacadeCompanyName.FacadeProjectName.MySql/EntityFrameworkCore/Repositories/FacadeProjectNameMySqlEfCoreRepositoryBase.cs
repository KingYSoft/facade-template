using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace FacadeCompanyName.FacadeProjectName.MySql.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// EF Core Repository base
    /// efcore 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public class FacadeProjectNameMySqlEfCoreRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<FacadeProjectNameMySqlDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected FacadeProjectNameMySqlEfCoreRepositoryBase(IDbContextProvider<FacadeProjectNameMySqlDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Add your common methods for all repositories
    }
}
