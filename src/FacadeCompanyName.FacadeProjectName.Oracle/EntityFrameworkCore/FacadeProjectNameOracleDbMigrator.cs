using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore
{
    public class FacadeProjectNameOracleDbMigrator : ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDbContextResolver _dbContextResolver;
        private readonly IAbpStartupConfiguration _abpStartupConfiguration;

        public FacadeProjectNameOracleDbMigrator(IUnitOfWorkManager unitOfWorkManager, IDbContextResolver dbContextResolver,
            IAbpStartupConfiguration abpStartupConfiguration)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _dbContextResolver = dbContextResolver;
            _abpStartupConfiguration = abpStartupConfiguration;
        }
        public virtual void CreateOrMigrate()
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                //using (var dbContext = _unitOfWorkManager.Current.GetDbContext<TDbContext>(MultiTenancySides.Host))
                using (var dbContext = _dbContextResolver.Resolve<FacadeProjectNameOracleDbContext>(_abpStartupConfiguration.DefaultNameOrConnectionString, null))
                {
                    dbContext.Database.Migrate();
                    _unitOfWorkManager.Current.SaveChanges();
                    uow.Complete();
                }
            }
        }
    }
}
