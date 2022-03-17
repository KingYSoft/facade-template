using Facade.Core.Web;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.App;
using System;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Application.Health
{
    public class HealthApplication : FacadeProjectNameApplicationBase, IHealthApplication
    {
        private readonly IAppRepository _appQueryRepository;
        public HealthApplication(IAppRepository appQueryRepository, IHttpRequestHeaderParser httpRequestHeaderParser) : base(httpRequestHeaderParser)
        {
            _appQueryRepository = appQueryRepository;
        }

        public async Task<DateTime> Check()
        {
            return await _appQueryRepository.ExecuteScalarAsync<DateTime>("select sysdate from dual");
        }
    }
}
