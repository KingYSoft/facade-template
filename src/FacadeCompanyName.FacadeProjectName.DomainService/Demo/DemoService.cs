using FacadeCompanyName.FacadeProjectName.DomainService.Demo.Dto;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.Demo;
using System;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Demo
{
    public class DemoService : FacadeProjectNameDomainServiceBase, IDemoService
    {
        private readonly IDemoRepository _demoRepository;
        private readonly IAppQueryRepository _appQueryRepository;
        public DemoService(IDemoRepository demoRepository, IAppQueryRepository appQueryRepository)
        {
            _demoRepository = demoRepository;
            _appQueryRepository = appQueryRepository;
        }
        public async Task<string> Check(CheckInput input)
        {
            var d = await _demoRepository.GetAsync(input.Id);
            return d?.Name;//.SysDate.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public class DualQuery_
        {
            public DateTime SysDate { get; set; }
        }
        public async Task<string> Query(int id)
        {
            return await _appQueryRepository.QueryFirstOrDefaultAsync<string>("select Name from demo where id = :id", new { id });
        }
    }
}
