using Facade.AspNetCore.Mvc.Authorization;
using Facade.Core.Web;
using FacadeCompanyName.FacadeProjectName.Application.Health;
using FacadeCompanyName.FacadeProjectName.Web.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Web.Host.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [Route("health")]
    public class HealthController : FacadeProjectNameControllerBase
    {
        private readonly IHealthApplication _healthApplication;
        public HealthController(IHealthApplication healthApplication)
        {
            _healthApplication = healthApplication;
        }

        [Route("check")]
        [HttpGet]
        [NoToken]
        public async Task<JsonResponse<string>> Check()
        {
            return new JsonResponse<string>(true, L("WelcomeMessage"))
            {
                Data = await _healthApplication.Check()
            };
        }
    }
}
