using FacadeCompanyName.FacadeProjectName.DomainService.Demo;
using FacadeCompanyName.FacadeProjectName.DomainService.Demo.Dto;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Application.Demo
{
    public class DemoApplication : FacadeProjectNameApplicationBase, IDemoApplication
    {
        private readonly IDemoService _demoService;
        public DemoApplication(IDemoService demoService)
        {
            _demoService = demoService;
        }

        public async Task<string> Check(CheckInput input)
        {
            return await _demoService.Check(input);
        }
        public async Task<string> Query(int id)
        {
            return await _demoService.Query(id);
        }
    }
}
