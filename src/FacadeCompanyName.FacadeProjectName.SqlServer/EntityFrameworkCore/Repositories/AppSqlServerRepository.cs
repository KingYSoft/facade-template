using Dapper;
using Facade.Dapper;
using Facade.Dapper.SqlServer;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.App;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.Interceptors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.SqlServer.EntityFrameworkCore.Repositories
{
    public class AppSqlServerRepository : SqlServerQueryRepository, IAppSqlServerRepository
    {
        public AppSqlServerRepository(IFacadeConnectionProvider facadeConnectionProvider)
            : base(facadeConnectionProvider)
        {
        }

        [DapperRepositoryInterceptor]
        public override Task<int> ExecuteAsync(string query, object param = null)
        {
            return base.ExecuteAsync(query, param);
        }
        [DapperRepositoryInterceptor]
        public override Task<T> ExecuteScalarAsync<T>(string query, object param = null)
        {
            return base.ExecuteScalarAsync<T>(query, param);
        }
        [DapperRepositoryInterceptor]
        public override Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null)
        {
            return base.QueryAsync<T>(query, param);
        }
        [DapperRepositoryInterceptor]
        public override Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> map, object param = null)
        {
            return base.QueryAsync(query, map, param);
        }
        [DapperRepositoryInterceptor]
        public override Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string query, Func<TFirst, TSecond, TThird, TReturn> map, object param = null)
        {
            return base.QueryAsync(query, map, param);
        }
        [DapperRepositoryInterceptor]
        public override Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null)
        {
            return base.QueryAsync(query, map, param);
        }
        [DapperRepositoryInterceptor]
        public override Task<T> QueryFirstOrDefaultAsync<T>(string query, object param = null)
        {
            return base.QueryFirstOrDefaultAsync<T>(query, param);
        }
        [DapperRepositoryInterceptor]
        public override Task<SqlMapper.GridReader> QueryMultipleAsync(string query, object param = null)
        {
            return base.QueryMultipleAsync(query, param);
        }
        [DapperRepositoryInterceptor]
        public override Task<(IEnumerable<T> list, int totalCount)> PagedAsync<T>(string query, string orderBy, int currentPage = 1, int pageCount = 10, object param = null)
        {
            return base.PagedAsync<T>(query, orderBy, currentPage, pageCount, param);
        }
    }
}
