using Abp.AspNetCore.Mvc.Extensions;
using Abp.Dependency;
using Abp.Extensions;
using Castle.Core.Logging;
using Facade;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.Filters
{
    public class RedLockActionFilter : IAsyncActionFilter, ISingletonDependency
    {
        private readonly ILogger _logger;
        private readonly RedLockFactory _redLockFactory;
        private readonly IFacadeConfiguration _facadeConfiguration;
        public RedLockActionFilter(ILogger logger, IFacadeConfiguration facadeConfiguration)
        {
            _logger = logger;
            _facadeConfiguration = facadeConfiguration;

            if (!_facadeConfiguration.Redis_Host.IsNullOrWhiteSpace())
            {
                var endPoints = new List<RedLockEndPoint>
                {
                    new DnsEndPoint(_facadeConfiguration.Redis_Host, _facadeConfiguration.Redis_Port),
                };
                _redLockFactory = RedLockFactory.Create(endPoints);
            }
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            if (!IsRedLock(context.ActionDescriptor.GetMethodInfo()))
            {
                await next();
                return;
            }
            if (_redLockFactory == null)
            {
                await next();
                return;
            }
            StringBuilder sb = new StringBuilder();
            var d_type = context.ActionDescriptor.GetMethodInfo().DeclaringType;
            var methodName = context.ActionDescriptor.GetMethodInfo().Name;
            sb.Append(d_type.FullName);
            sb.Append(methodName);
            //sb.Append(json);
            var redKey = StringExtensions.ToMd5(sb.ToString());
            bool isExecuted;
            isExecuted = await OverlappingWork($"__UOW_REDLOCKER__{redKey}", TimeSpan.FromSeconds(3), async () =>
            {
                await next();
            });
            if (!isExecuted)
            {
                var json = JsonConvert.SerializeObject(context.ActionArguments);
                _logger.Warn($"__UOW_REDLOCKER__:{d_type.FullName}-{methodName}-{json}");
                throw new AppException("数据正在处理中，请稍后，重新提交...");
            }
        }

        private bool IsRedLock(MethodInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(true).OfType<RedLockAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return true;
            }

            attrs = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true).OfType<RedLockAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 阻塞式调用，事情最终会被调用（等待时间内）
        /// </summary>
        /// <param name="resource">锁定资源的标识</param>
        /// <param name="expiryTime">锁过期时间</param>
        /// <param name="waitTime">等待时间</param> 
        private async Task<bool> BlockingWork(string resource, TimeSpan expiryTime, TimeSpan waitTime, Action work)
        {
            // resource 锁定的对象
            // expiryTime 锁定过期时间，锁区域内的逻辑执行如果超过过期时间，锁将被释放
            // waitTime 等待时间,相同的 resource 如果当前的锁被其他线程占用,最多等待时间
            // retryTime 等待时间内，多久尝试获取一次
            await using (var redisLock = await _redLockFactory.CreateLockAsync(resource, expiryTime, waitTime, TimeSpan.FromMilliseconds(200)))
            {
                if (redisLock.IsAcquired)
                {
                    work.Invoke();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 跳过式调用，如果事情正在被调用，直接跳过
        /// </summary>
        /// <param name="resource">锁定资源的标识</param>
        /// <param name="expiryTime">锁过期时间</param>
        private async Task<bool> OverlappingWork(string resource, TimeSpan expiryTime, Action work)
        {
            await using (var redisLock = await _redLockFactory.CreateLockAsync(resource, expiryTime))
            {
                if (redisLock.IsAcquired)
                {
                    work.Invoke();
                    return true;
                }
                return false;
            }
        }
    }
}
