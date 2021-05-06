using Abp.BackgroundJobs;
using FacadeCompanyName.FacadeProjectName.DomainService.Interceptors;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    /// <summary>
    /// 后台工作队列
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    [JobInterceptor]
    public abstract class FacadeProjectNameBackgroundJobBase<TArgs> : AsyncBackgroundJob<TArgs>
    {
        protected FacadeProjectNameBackgroundJobBase() : base()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
