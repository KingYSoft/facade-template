using Facade.AspNetCore.Mvc.Authorization;
using Facade.AspNetCore.Web.Models;
using FacadeCompanyName.FacadeProjectName.Application.Demo;
using FacadeCompanyName.FacadeProjectName.DomainService.Demo.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Web.Host.Controllers
{
    [Route("demo")]
    public class DemoController : FacadeProjectNameControllerBase
    {
        private readonly IDemoApplication _demoApplication;
        public DemoController(IDemoApplication demoApplication)
        {
            _demoApplication = demoApplication;
        }

        [Route("health")]
        [HttpGet]
        [NoToken]
        public async Task<JsonResponse> Health()
        {
            await _demoApplication.Health();
            return await Task.FromResult(new JsonResponse(true, L("WelcomeMessage")));
        }

        [Route("check")]
        [HttpPost]
        public async Task<JsonResponse<string>> Check([FromBody]CheckInput input)
        {
            return new JsonResponse<string>(true, L("WelcomeMessage"))
            {
                Data = await _demoApplication.Check(input)
            };
        }
        [Route("query")]
        [HttpGet]
        public async Task<JsonResponse<string>> Query([FromQuery] int id)
        {
            return new JsonResponse<string>(true, L("WelcomeMessage"))
            {
                Data = await _demoApplication.Query(id)
            };
        }
    }
}
