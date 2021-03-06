﻿using Abp.Runtime.Session;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    public abstract class FacadeProjectNameDomainServiceBase : Abp.Domain.Services.DomainService,
        IFacadeProjectNameDomainServiceBase
    {
        public IAbpSession AbpSession { get; set; }
        protected FacadeProjectNameDomainServiceBase()
            : base()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
            AbpSession = NullAbpSession.Instance;
        }
    }
}
