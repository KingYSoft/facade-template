using Abp.Quartz;
using FacadeCompanyName.FacadeProjectName.DomainService.Interceptors;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    [JobInterceptor]
    public abstract class FacadeProjectNameScheduleJobBase : JobBase
    {
        protected FacadeProjectNameScheduleJobBase() : base()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
