﻿using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using Microsoft.AspNetCore.Mvc;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.Controllers
{
    [ApiController]
    [AbpMvcAuthorize]
    public abstract class FacadeProjectNameControllerBase : AbpController
    {
        protected FacadeProjectNameControllerBase()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
