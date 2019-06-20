using FacadeCompanyName.FacadeProjectName.DomainService.Demo.Dto;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.Demo;
using System;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Demo
{
    public class DemoService : FacadeProjectNameDomainServiceBase, IDemoService
    {
        private readonly IDemoRepository _demoRepository;
        public DemoService(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;
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
    }
}
