using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore
{
    public class FacadeProjectNameDbMigrator : ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDbContextResolver _dbContextResolver;

        public FacadeProjectNameDbMigrator(IUnitOfWorkManager unitOfWorkManager, IDbContextResolver dbContextResolver)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _dbContextResolver = dbContextResolver;
        }
        public virtual void CreateOrMigrate<TDbContext>(string conn)
           where TDbContext : DbContext
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                //using (var dbContext = _unitOfWorkManager.Current.GetDbContext<TDbContext>(MultiTenancySides.Host))
                using (var dbContext = _dbContextResolver.Resolve<TDbContext>(conn, null))
                {
                    dbContext.Database.Migrate();
                    _unitOfWorkManager.Current.SaveChanges();
                    uow.Complete();
                }
            }
        }
    }
}
