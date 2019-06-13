using Abp.Authorization;
using Facade.AspNetCore.Web.Models;
using FacadeCompanyName.FacadeProjectName.Oracle;
using FacadeCompanyName.FacadeProjectName.Web.Host.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Web.Host.Controllers
{
    [Route("demo")]
    public class DemoController : FacadeProjectNameControllerBase
    {
        private readonly IFacadeProjectNameOracleRepository _oracleRepository;
        public DemoController(IFacadeProjectNameOracleRepository oracleRepository)
        {
            _oracleRepository = oracleRepository;
        }
        [Route("check")]
        [HttpPost]
        [AbpAuthorize]
        public async Task<JsonResponse<string>> Check([FromBody]CheckInput input)
        {
            var a = await _oracleRepository.QueryAsync<DualQuery_>(@"
select sysdate  from dual  where 1=:id
", new { id = input.Id });
            var time = a.FirstOrDefault()?.SysDate.ToString("yyyy-MM-dd HH:mm:ss");
            return new JsonResponse<string>(true, L("WelcomeMessage"))
            {
                Data = time
            };
        }
        public class DualQuery_
        {
            public DateTime SysDate { get; set; }
        }
    }
}
