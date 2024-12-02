using Abp.Domain.Entities;
using Facade.Dapper;
using Facade.Dapper.MySql;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FacadeCompanyName.FacadeProjectName.MySql.EntityFrameworkCore.Repositories
{
    public class FacadeProjectNameMySqlDapperRepositoryBase<TEntity, TPrimaryKey> : MySqlDapperRepository<TEntity, TPrimaryKey>
         where TEntity : class, IEntity<TPrimaryKey>
    {
        protected FacadeProjectNameMySqlDapperRepositoryBase(IFacadeConnectionProvider facadeConnection)
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
