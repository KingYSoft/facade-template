using Abp.Application.Services;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.Application
{
    public abstract class FacadeProjectNameApplicationBase : ApplicationService, IFacadeProjectNameApplicationBase
    {
        protected FacadeProjectNameApplicationBase()
            : base()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
