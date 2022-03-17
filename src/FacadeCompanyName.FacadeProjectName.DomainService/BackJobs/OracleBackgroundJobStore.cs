//using Abp.BackgroundJobs;
//using Abp.Dependency;
//using Abp.Domain.Uow;
//using Abp.Timing;
//using FacadeCompanyName.FacadeProjectName.DomainService.Share;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FacadeCompanyName.FacadeProjectName.DomainService.BackJobs
//{
//    public class OracleBackgroundJobStore : IBackgroundJobStore, ISingletonDependency
//    {
//        private readonly IAppRepository _appRepository;
//        private readonly ISysBackgroundJobRepository _backgroundJobRepository; // VHTSYS_BACKGROUND_JOB
//        private readonly IFacadeConfiguration _configuration;
//        public OracleBackgroundJobStore(ISysBackgroundJobRepository backgroundJobRepository,
//            IAppRepository appRepository,
//            IFacadeConfiguration configuration)
//        {
//            _appRepository = appRepository;
//            _backgroundJobRepository = backgroundJobRepository;
//            _configuration = configuration;
//        }
//        public async Task<BackgroundJobInfo> GetAsync(long jobId)
//        {
//            var appEnv = Environment.MachineName + "_" + _configuration.AppName;
//            var entity = await _backgroundJobRepository.FirstOrDefaultAsync(x => x.Id == jobId && x.appevn == appEnv);
//            if (entity == null)
//                return null;
//            return entity.ToInfo();
//        }
//        public BackgroundJobInfo Get(long jobId)
//        {
//            return GetAsync(jobId).GetAwaiter().GetResult();
//        }
//        public async Task InsertAsync(BackgroundJobInfo jobInfo)
//        {
//            var appEnv = Environment.MachineName + "_" + _configuration.AppName;
//            var entity = jobInfo.ToJob();
//            entity.appevn = appEnv;
//            var id = await _backgroundJobRepository.InsertAndGetIdAsync(entity);
//            jobInfo.Id = (long)id;
//        }
//        public void Insert(BackgroundJobInfo jobInfo)
//        {
//            InsertAsync(jobInfo).GetAwaiter().GetResult();
//        }

//        [UnitOfWork]
//        public virtual async Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
//        {
//            var appEnv = Environment.MachineName + "_" + _configuration.AppName;
//            var waitingJobs = (await _backgroundJobRepository.GetAllAsync(t => t.appevn == appEnv && t.isabandoned != 1 && t.nexttrytime <= Clock.Now))
//                //  .Where(t => t.isabandoned != 1 && t.nexttrytime <= Clock.Now)
//                .OrderByDescending(t => t.priority)
//                .ThenBy(t => t.trycount)
//                .ThenBy(t => t.nexttrytime)
//                .Take(maxResultCount)
//                .ToList();

//            var list = new List<BackgroundJobInfo>();
//            foreach (var job in waitingJobs)
//            {
//                list.Add(job.ToInfo());
//            }
//            return list;
//        }

//        [UnitOfWork]
//        public List<BackgroundJobInfo> GetWaitingJobs(int maxResultCount)
//        {
//            return GetWaitingJobsAsync(maxResultCount).GetAwaiter().GetResult();
//        }

//        public async Task DeleteAsync(BackgroundJobInfo jobInfo)
//        {
//            if (jobInfo == null)
//                return;
//            var appEnv = Environment.MachineName + "_" + _configuration.AppName;
//            await _backgroundJobRepository.DeleteAsync(x => x.Id == jobInfo.Id && x.appevn == appEnv);
//        }

//        public void Delete(BackgroundJobInfo jobInfo)
//        {
//            DeleteAsync(jobInfo).GetAwaiter().GetResult();
//        }

//        public async Task UpdateAsync(BackgroundJobInfo jobInfo)
//        {
//            var appEnv = Environment.MachineName + "_" + _configuration.AppName;
//            var entity = jobInfo.ToJob();
//            entity.appevn = appEnv;
//            await _backgroundJobRepository.UpdateAsync(entity);
//        }
//        public void Update(BackgroundJobInfo jobInfo)
//        {
//            UpdateAsync(jobInfo).GetAwaiter().GetResult();
//        }
//    }

//    public static class JobConvert
//    {
//        public static VHTSYS_BACKGROUND_JOB ToJob(this BackgroundJobInfo info)
//        {
//            return new VHTSYS_BACKGROUND_JOB
//            {
//                creationtime = info.CreationTime,
//                creatoruserid = (info.CreatorUserId ?? -1).ToString(),
//                Id = info.Id,
//                isabandoned = Convert.ToDecimal(info.IsAbandoned),
//                jobargs = info.JobArgs,
//                jobtype = info.JobType,
//                lasttrytime = info.LastTryTime,
//                nexttrytime = info.NextTryTime,
//                priority = (decimal)info.Priority,
//                trycount = info.TryCount,
//            };
//        }
//        public static BackgroundJobInfo ToInfo(this VHTSYS_BACKGROUND_JOB job)
//        {
//            long? userid = null;
//            if (long.TryParse(job.creatoruserid, out long user))
//            {
//                userid = user;
//            }
//            return new BackgroundJobInfo
//            {
//                CreationTime = job.creationtime,
//                CreatorUserId = userid,
//                Id = (long)job.Id,
//                IsAbandoned = Convert.ToBoolean(job.isabandoned),
//                JobArgs = job.jobargs,
//                JobType = job.jobtype,
//                LastTryTime = job.lasttrytime,
//                NextTryTime = job.nexttrytime,
//                Priority = (BackgroundJobPriority)job.priority,
//                TryCount = (short)job.trycount,
//            };
//        }
//    }
//}
