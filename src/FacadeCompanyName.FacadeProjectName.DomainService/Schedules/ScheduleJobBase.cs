using Abp.Quartz;
using Facade.Quartz;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Schedules
{
    /// <summary>
    /// 任务调度作业
    /// </summary>
    public abstract class ScheduleJobBase : FacadeScheduleJobBase
    {
        protected ScheduleJobBase() : base()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
