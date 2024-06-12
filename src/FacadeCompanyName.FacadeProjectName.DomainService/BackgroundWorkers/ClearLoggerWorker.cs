using Abp.Dependency;
using Abp.Threading.Timers;
using Abp.Timing;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.BackgroundWorkers
{
    public class ClearLoggerWorker: FacadeProjectNameBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IFacadeConfiguration _facadeConfiguration;
        public ClearLoggerWorker(AbpAsyncTimer timer, IFacadeConfiguration facadeConfiguration)
            : base(timer)
        {
            timer.Period = 1000 * 60 * 60;
            _facadeConfiguration = facadeConfiguration;
        }
        protected override async Task DoWorkAsync()
        {
            var rootPath = _facadeConfiguration.AppRootPath;
            var logsPath = Path.Combine(rootPath, "App_Data", "Logs");
            if (Directory.Exists(logsPath))
            {
                var lastMonth = Clock.Now.Subtract(TimeSpan.FromDays(90));
                var path1 = Path.Combine(logsPath, lastMonth.ToString("yyyy"), lastMonth.ToString("MM"));
                if (Directory.Exists(path1))
                {
                    Directory.Delete(path1, true);
                }
            }
            await Task.CompletedTask;
        }
    }
}
