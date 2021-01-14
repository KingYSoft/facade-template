using Dapper;
using Facade.Dapper;
using Facade.Dapper.Oracle;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore.Interceptors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore.Repositories
{
    public class AppRepository : OracleQueryRepository, IAppRepository
    {
        public AppRepository(IFacadeConnectionProvider facadeConnectionProvider)
            : base(facadeConnectionProvider)
        {
        }
        [RepositoryInterceptor]
        public override Task<int> ExecuteAsync(string query, object param = null)
        {
            return base.ExecuteAsync(query, param);
        }
        [RepositoryInterceptor]
        public override Task<T> ExecuteScalarAsync<T>(string query, object param = null)
        {
            return base.ExecuteScalarAsync<T>(query, param);
        }
        [RepositoryInterceptor]
        public override Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null)
        {
            return base.QueryAsync<T>(query, param);
        }
        [RepositoryInterceptor]
        public override Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> map, object param = null)
        {
            return base.QueryAsync<TFirst, TSecond, TReturn>(query, map, param);
        }
        [RepositoryInterceptor]
        public override Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string query, Func<TFirst, TSecond, TThird, TReturn> map, object param = null)
        {
            return base.QueryAsync<TFirst, TSecond, TThird, TReturn>(query, map, param);
        }
        [RepositoryInterceptor]
        public override Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null)
        {
            return base.QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(query, map, param);
        }
        [RepositoryInterceptor]
        public override Task<T> QueryFirstOrDefaultAsync<T>(string query, object param = null)
        {
            return base.QueryFirstOrDefaultAsync<T>(query, param);
        }
        [RepositoryInterceptor]
        public override Task<SqlMapper.GridReader> QueryMultipleAsync(string query, object param = null)
        {
            return base.QueryMultipleAsync(query, param);
        }
        [RepositoryInterceptor]
        public override Task<(IEnumerable<T> list, int totalCount)> PagedAsync<T>(string query, int currentPage = 1, int pageCount = 10, object param = null)
        {
            return base.PagedAsync<T>(query, currentPage, pageCount, param);
        }
    }
}
