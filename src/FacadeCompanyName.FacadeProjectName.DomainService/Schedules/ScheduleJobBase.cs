using Abp.Quartz;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Schedules
{
    /// <summary>
    /// 任务调度作业
    /// </summary>
    public abstract class ScheduleJobBase : JobBase
    {
        protected ScheduleJobBase() : base()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
