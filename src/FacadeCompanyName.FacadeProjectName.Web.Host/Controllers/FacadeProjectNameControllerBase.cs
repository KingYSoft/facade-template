using Abp.AspNetCore.Mvc.Controllers;
using FacadeCompanyName.FacadeProjectName.DomainService;
using Microsoft.AspNetCore.Mvc;

namespace FacadeCompanyName.FacadeProjectName.Web.Host.Controllers
{
    [ApiController]
    public abstract class FacadeProjectNameControllerBase : AbpController
    {
        protected FacadeProjectNameControllerBase()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
