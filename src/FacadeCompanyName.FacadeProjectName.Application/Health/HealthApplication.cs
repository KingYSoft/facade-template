using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using System;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Application.Health
{
    public class HealthApplication : FacadeProjectNameApplicationBase, IHealthApplication
    {
        private readonly IAppRepository _appQueryRepository;
        public HealthApplication(IAppRepository appQueryRepository)
        {
            _appQueryRepository = appQueryRepository;
        }

        public async Task<DateTime> Check()
        {
            return await _appQueryRepository.ExecuteScalarAsync<DateTime>("select sysdate from dual");
        }
    }
}
