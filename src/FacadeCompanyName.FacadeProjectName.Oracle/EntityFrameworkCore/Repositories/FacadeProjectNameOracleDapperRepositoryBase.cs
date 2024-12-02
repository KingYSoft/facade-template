using Abp.Domain.Entities;
using Facade.Dapper;
using Facade.Dapper.Oracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore.Repositories
{
    public class FacadeProjectNameOracleDapperRepositoryBase<TEntity, TPrimaryKey> : OracleDapperRepository<TEntity, TPrimaryKey>
         where TEntity : class, IEntity<TPrimaryKey>
    {
        protected FacadeProjectNameOracleDapperRepositoryBase(IFacadeConnectionProvider facadeConnection)
              : base(facadeConnection)
        {
        }
        public override IEnumerable<TEntity> GetAll()
        {
            var list = base.GetAll();
            return list.ToList();
        }
        public override IEnumerable<TEntity> GetAll([NotNull] Expression<Func<TEntity, bool>> predicate)
        {
            var list = base.GetAll(predicate);
            return list.ToList();
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return base.InsertAndGetId(entity);
        }
        public override void Update(TEntity entity)
        {
            base.Update(entity);
        }
        public override void Delete(TEntity entity)
        {
            base.Delete(entity);
        }
    }
}
