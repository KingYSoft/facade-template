﻿using Abp.AspNetCore.Mvc.Extensions;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Json;
using Abp.Runtime.Caching;
using Abp.Threading.Extensions;
using Castle.Core.Logging;
using Facade.AspNetCore.Mvc.Authorization;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.Filters
{
    public class DebounceActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly ILogger _logger;
        private readonly IFacadeConfiguration _facadeConfiguration;
        private readonly IAppRepository _appRepository;
        private readonly ICacheManager _cacheManager;
        public DebounceActionFilter(ILogger logger, IFacadeConfiguration facadeConfiguration, IAppRepository appRepository, ICacheManager cacheManager)
        {
            _logger = logger;
            _facadeConfiguration = facadeConfiguration;
            _appRepository = appRepository;
            _cacheManager = cacheManager;
        }
        private ITypedCache<string, string> DebounceCache =>
            _cacheManager.GetCache<string, string>(FacadeProjectNameConsts.DebounceCacheName);

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }
            var methodInfo = context.ActionDescriptor.GetMethodInfo();
            var attrs = methodInfo.GetCustomAttributes(true).OfType<NoTokenAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                await next();
                return;
            }

            attrs = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true).OfType<NoTokenAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                await next();
                return;
            }


            if (!IsDebounce(methodInfo))
            {
                await next();
                return;
            }

            try
            {
                var request = context.HttpContext.Request;
                var requestParams = context.ActionArguments;
                #region 获取参数及token
                var sb = new StringBuilder();
                sb.Append(request.Method);

                foreach (var kvp in requestParams)
                {
                    sb.Append(kvp.Key).Append(kvp.Value.ToJsonString());
                }
                sb.Append(request.Host.ToString());
                sb.Append(request.Path.ToString());
                sb.Append(request.QueryString.ToString());
                sb.Append(request.Protocol);
                if (request.Headers.TryGetValue("Authorization", out var v))
                {
                    var token = v.ToString();
                    sb.Append(token);
                }
                #endregion

                var cacheKey = sb.ToString().ToMd5();
                using (await DebounceLock.SemaphoreSlim.LockAsync())
                {
                    var cacheValue = await DebounceCache.GetOrDefaultAsync(cacheKey);
                    if (cacheValue.IsNullOrWhiteSpace())
                    {
                        var result = await next();
                        if (result.Result is ObjectResult)
                        {
                            var res = result.Result as ObjectResult;
                            await DebounceCache.SetAsync(cacheKey, JsonSerializationHelper.SerializeWithType(res.Value), TimeSpan.FromSeconds(2));
                        }
                    }
                    else
                    {
                        var obj = new ObjectResult(JsonSerializationHelper.DeserializeWithType(cacheValue));
                        context.Result = obj;
                        var d_type = context.ActionDescriptor.GetMethodInfo().DeclaringType;
                        var methodName = context.ActionDescriptor.GetMethodInfo().Name;
                        _logger.Debug($"Debounce-{d_type.FullName}-{methodName}：" + cacheKey);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(ex.Message, ex);
                await next();
            }

        }

        private static bool IsDebounce(MethodInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(true).OfType<DebounceAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return true;
            }

            attrs = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true).OfType<DebounceAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return true;
            }

            return false;
        }
    }

    public static class DebounceLock
    {
        public static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
    }
}
