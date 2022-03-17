using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.DomainService.BackgroundWorkers
{
    /// <summary>
    /// 后台工人
    /// </summary>
    public abstract class FacadeProjectNameBackgroundWorkerBase : PeriodicBackgroundWorkerBase
    {
        protected FacadeProjectNameBackgroundWorkerBase(AbpTimer timer)
             : base(timer)
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
