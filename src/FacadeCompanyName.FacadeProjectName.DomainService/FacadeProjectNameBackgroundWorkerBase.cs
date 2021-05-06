using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using FacadeCompanyName.FacadeProjectName.DomainService.Interceptors;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    /// <summary>
    /// 后台工人
    /// </summary>
    [JobInterceptor]
    public abstract class FacadeProjectNameBackgroundWorkerBase : PeriodicBackgroundWorkerBase
    {
        protected FacadeProjectNameBackgroundWorkerBase(AbpTimer timer)
              : base(timer)
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
