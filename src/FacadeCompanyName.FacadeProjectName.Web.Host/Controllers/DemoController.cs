﻿using Abp.Authorization;
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
        [Route("check")]
        [HttpPost]
        [AbpAuthorize]
        public async Task<JsonResponse<string>> Check([FromBody]CheckInput input)
        {
            return new JsonResponse<string>(true, L("WelcomeMessage"))
            {
                Data = await _demoApplication.Check(input)
            };
        }
    }
}
