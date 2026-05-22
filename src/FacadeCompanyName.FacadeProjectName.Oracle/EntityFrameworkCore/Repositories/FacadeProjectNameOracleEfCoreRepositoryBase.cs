using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// EF Core Repository base
    /// efcore 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public class FacadeProjectNameOracleEfCoreRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<FacadeProjectNameOracleDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected FacadeProjectNameOracleEfCoreRepositoryBase(IDbContextProvider<FacadeProjectNameOracleDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Add your common methods for all repositories


        public override TEntity Insert(TEntity entity)
        {
            return base.Insert(entity);
        }
        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return base.InsertAsync(entity);
        }
        public override TEntity Update(TEntity entity)
        {
            return base.Update(entity);
        }
        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}
