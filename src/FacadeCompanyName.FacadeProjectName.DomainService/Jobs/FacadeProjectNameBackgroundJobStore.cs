using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Timing;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Jobs
{
    public class FacadeProjectNameBackgroundJobStore : IBackgroundJobStore, ISingletonDependency
    {
        private readonly IBackJobRepository _backJobRepository;
        private readonly IFacadeConfiguration _configuration;
        public FacadeProjectNameBackgroundJobStore(IBackJobRepository backJobRepository, IFacadeConfiguration configuration)
        {
            _backJobRepository = backJobRepository;
            _configuration = configuration;
        }
        public async Task<BackgroundJobInfo> GetAsync(long jobId)
        {
            var appEnv = Environment.MachineName + "_" + _configuration.AppName;
            var entity = await _backJobRepository.FirstOrDefaultAsync(x => x.Id == jobId && x.AppEnv == appEnv);
            if (entity == null)
                return null;
            return entity.ToInfo();
        }
        public BackgroundJobInfo Get(long jobId)
        {
            return GetAsync(jobId).GetAwaiter().GetResult();
        }
        public async Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            var appEnv = Environment.MachineName + "_" + _configuration.AppName;
            var entity = jobInfo.ToJob();
            entity.AppEnv = appEnv;
            var id = await _backJobRepository.InsertAndGetIdAsync(entity);
            jobInfo.Id = id;
        }
        public void Insert(BackgroundJobInfo jobInfo)
        {
            InsertAsync(jobInfo).GetAwaiter().GetResult();
        }

        public virtual async Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
        {
            var appEnv = Environment.MachineName + "_" + _configuration.AppName;
            var waitingJobs = (await _backJobRepository.GetAllAsync(t => t.AppEnv == appEnv && t.IsAbandoned != true && t.NextTryTime <= Clock.Now))
                //  .Where(t => t.isabandoned != 1 && t.nexttrytime <= Clock.Now)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.TryCount)
                .ThenBy(t => t.NextTryTime)
                .Take(maxResultCount)
                .ToList();

            var list = new List<BackgroundJobInfo>();
            foreach (var job in waitingJobs)
            {
                list.Add(job.ToInfo());
            }
            return list;
        }
        public List<BackgroundJobInfo> GetWaitingJobs(int maxResultCount)
        {
            return GetWaitingJobsAsync(maxResultCount).GetAwaiter().GetResult();
        }

        public async Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            if (jobInfo == null)
                return;
            var appEnv = Environment.MachineName + "_" + _configuration.AppName;
            await _backJobRepository.DeleteAsync(x => x.Id == jobInfo.Id && x.AppEnv == appEnv);
        }

        public void Delete(BackgroundJobInfo jobInfo)
        {
            DeleteAsync(jobInfo).GetAwaiter().GetResult();
        }

        public async Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            var appEnv = Environment.MachineName + "_" + _configuration.AppName;
            var entity = jobInfo.ToJob();
            entity.AppEnv = appEnv;
            await _backJobRepository.UpdateAsync(entity);
        }
        public void Update(BackgroundJobInfo jobInfo)
        {
            UpdateAsync(jobInfo).GetAwaiter().GetResult();
        }
    }

    /// <summary>
    /// 在这里用不了 AutoMaper
    /// </summary>
    public static class JobConvert
    {
        public static BackJob ToJob(this BackgroundJobInfo info)
        {
            return new BackJob
            {
                CreationTime = info.CreationTime,
                CreatorUserId = info.CreatorUserId,
                Id = info.Id,
                IsAbandoned = info.IsAbandoned,
                JobArgs = info.JobArgs,
                JobType = info.JobType,
                LastTryTime = info.LastTryTime,
                NextTryTime = info.NextTryTime,
                Priority = info.Priority,
                TryCount = info.TryCount,
            };
        }
        public static BackgroundJobInfo ToInfo(this BackJob job)
        {
            return new BackgroundJobInfo
            {
                CreationTime = job.CreationTime,
                CreatorUserId = job.CreatorUserId,
                Id = job.Id,
                IsAbandoned = job.IsAbandoned,
                JobArgs = job.JobArgs,
                JobType = job.JobType,
                LastTryTime = job.LastTryTime,
                NextTryTime = job.NextTryTime,
                Priority = job.Priority,
                TryCount = job.TryCount,
            };
        }
    }
}