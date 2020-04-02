using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using System;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Application.Health
{
    public class HealthApplication : FacadeProjectNameApplicationBase, IHealthApplication
    {
        private readonly IAppQueryRepository _appQueryRepository;
        public HealthApplication(IAppQueryRepository appQueryRepository)
        {
            _appQueryRepository = appQueryRepository;
        }

        public async Task<DateTime> Check()
        {
            return await _appQueryRepository.ExecuteScalarAsync<DateTime>("select sysdate from dual");
        }
    }
}
