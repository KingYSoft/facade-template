﻿using FacadeCompanyName.FacadeProjectName.DomainService.Share.App;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Application.Health
{
    public class HealthApplication : FacadeProjectNameApplicationBase, IHealthApplication
    {
        private readonly IAppOracleRepository _appQueryRepository;
        public HealthApplication(IAppOracleRepository appQueryRepository)
        {
            _appQueryRepository = appQueryRepository;
        }

        public async Task<string> Check()
        {
            // oracle sql
            return await _appQueryRepository.ExecuteScalarAsync<string>("select to_char(sysdate, 'yyyy-mm-dd hh24:mi:ss') from dual");
        }
    }
}
