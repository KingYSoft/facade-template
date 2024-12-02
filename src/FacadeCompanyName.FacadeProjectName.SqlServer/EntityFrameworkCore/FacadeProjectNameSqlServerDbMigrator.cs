using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace FacadeCompanyName.FacadeProjectName.SqlServer.EntityFrameworkCore
{
    public class FacadeProjectNameSqlServerDbMigrator : ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDbContextResolver _dbContextResolver;
        private readonly IFacadeConfiguration _facadeConfiguration;

        public FacadeProjectNameSqlServerDbMigrator(IUnitOfWorkManager unitOfWorkManager, IDbContextResolver dbContextResolver,
            IFacadeConfiguration facadeConfiguration)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _dbContextResolver = dbContextResolver;
            _facadeConfiguration = facadeConfiguration;
        }
        public virtual void CreateOrMigrate()
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                //using (var dbContext = _unitOfWorkManager.Current.GetDbContext<TDbContext>(MultiTenancySides.Host))
                using (var dbContext = _dbContextResolver.Resolve<FacadeProjectNameSqlServerDbContext>(_facadeConfiguration.SqlServerConnString, null))
                {
                    dbContext.Database.Migrate();
                    _unitOfWorkManager.Current.SaveChanges();
                    uow.Complete();
                }
            }
        }
    }
}
