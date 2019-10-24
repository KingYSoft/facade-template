using FacadeCompanyName.FacadeProjectName.DomainService.Demo;
using FacadeCompanyName.FacadeProjectName.DomainService.Demo.Dto;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using System;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Application.Demo
{
    public class DemoApplication : FacadeProjectNameApplicationBase, IDemoApplication
    {
        private readonly IDemoService _demoService;
        private readonly IAppQueryRepository _appQueryRepository;
        public DemoApplication(IDemoService demoService, IAppQueryRepository appQueryRepository)
        {
            _demoService = demoService;
            _appQueryRepository = appQueryRepository;
        }

        public async Task<string> Check(CheckInput input)
        {
            return await _demoService.Check(input);
        }
        public async Task<string> Query(int id)
        {
            return await _demoService.Query(id);
        }
        public async Task Health()
        {
            await _appQueryRepository.QueryAsync<DateTime>("select sysdate from dual");
        }
    }
}
