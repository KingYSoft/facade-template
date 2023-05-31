using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.DomainService.BackgroundWorkers
{
    /// <summary>
    /// 后台工人
    /// </summary>
    public abstract class FacadeProjectNameBackgroundWorkerBase : AsyncPeriodicBackgroundWorkerBase
    {
        protected FacadeProjectNameBackgroundWorkerBase(AbpAsyncTimer timer)
             : base(timer)
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;

            // Default value: 5000 (5 seconds). 
            Timer.Period = 5000;
        }
    }
}
