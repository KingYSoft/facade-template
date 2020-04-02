using Abp.BackgroundJobs;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share.Jobs
{
    [Table("BackJob")]
    public class BackJob : CreationAuditedEntity<long>
    {
        /// <summary>
        /// Maximum length of <see cref="JobType"/>.
        /// Value: 512.
        /// </summary>
        public const int MaxJobTypeLength = 512;

        /// <summary>
        /// Maximum length of <see cref="JobArgs"/>.
        /// Value: 1 MB (1,048,576 bytes).
        /// </summary>
        public const int MaxJobArgsLength = 1024 * 1024;



        /// <summary>
        /// Type of the job.
        /// It's AssemblyQualifiedName of job type.
        /// </summary>
        [Required]
        [StringLength(MaxJobTypeLength)]
        public virtual string JobType { get; set; }

        /// <summary>
        /// Job arguments as JSON string.
        /// </summary>
        [Required]
        [StringLength(MaxJobArgsLength)]
        public virtual string JobArgs { get; set; }

        /// <summary>
        /// Try count of this job.
        /// A job is re-tried if it fails.
        /// </summary>
        public virtual short TryCount { get; set; }

        /// <summary>
        /// Next try time of this job.
        /// </summary>
        //[Index("IX_IsAbandoned_NextTryTime", 2)]
        public virtual DateTime NextTryTime { get; set; }

        /// <summary>
        /// Last try time of this job.
        /// </summary>
        public virtual DateTime? LastTryTime { get; set; }

        /// <summary>
        /// This is true if this job is continously failed and will not be executed again.
        /// </summary>
        //[Index("IX_IsAbandoned_NextTryTime", 1)]
        public virtual bool IsAbandoned { get; set; }

        /// <summary>
        /// Priority of this job.
        /// </summary>
        public virtual BackgroundJobPriority Priority { get; set; }

        /// <summary>
        /// app env
        /// </summary>
        public virtual string AppEnv { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundJobInfo"/> class.
        /// </summary>
        public BackJob()
        {
            NextTryTime = Clock.Now;
            Priority = BackgroundJobPriority.Normal;
        }

    }
}