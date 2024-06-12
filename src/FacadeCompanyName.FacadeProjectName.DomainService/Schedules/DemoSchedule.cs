using Castle.Core.Logging;
using Quartz;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Schedules
{
    public class DemoSchedule : ScheduleJobBase, Abp.Dependency.ITransientDependency
    {
        private readonly ILogger _logger;
        public DemoSchedule(ILogger logger)
        {
            _logger = logger;
        }
        public override async Task ExecuteJobAsync(IJobExecutionContext context)
        {
            _logger.Debug("DemoSchedule 执行了");

            await Task.CompletedTask;
        }
    }
}
