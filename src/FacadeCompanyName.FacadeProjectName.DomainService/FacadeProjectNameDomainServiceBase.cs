namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    public abstract class FacadeProjectNameDomainServiceBase : Abp.Domain.Services.DomainService,
        IFacadeProjectNameDomainServiceBase
    {
        public FacadeProjectNameDomainServiceBase()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
