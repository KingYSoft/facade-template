using Abp.BackgroundJobs;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.DomainService.BackJobs
{
    /// <summary>
    /// 后台工作队列
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public abstract class BackJobBase<TArgs> : AsyncBackgroundJob<TArgs>
    {
        protected BackJobBase()
            : base()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
