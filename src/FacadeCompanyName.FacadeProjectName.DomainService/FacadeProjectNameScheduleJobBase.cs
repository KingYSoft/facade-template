﻿using Abp.Quartz;
using FacadeCompanyName.FacadeProjectName.DomainService.Interceptors;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.DomainService
{
    /// <summary>
    /// 任务调度作业
    /// </summary>
    [JobInterceptor]
    public abstract class FacadeProjectNameScheduleJobBase : JobBase
    {
        protected FacadeProjectNameScheduleJobBase() : base()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
    }
}
