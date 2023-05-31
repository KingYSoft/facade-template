using Abp.Dependency;
using Abp.Threading.Timers;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.BackgroundWorkers
{
    public class DemoWorker: FacadeProjectNameBackgroundWorkerBase, ISingletonDependency
    {
        public DemoWorker(AbpAsyncTimer timer)
            : base(timer)
        {             
        }
        protected override async Task DoWorkAsync()
        {
            // TODO
            Logger.Info("this is a demo worker.");
            await Task.CompletedTask;
        }
    }
}
