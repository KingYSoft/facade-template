using Abp.Dependency;
using Abp.Json;
using Abp.Threading;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Oracle.EntityFrameworkCore.Interceptors
{
    public class RepositoryInterceptor : IInterceptor
    {
        private readonly ILogger _logger;

        public RepositoryInterceptor()
        {
            _logger = IocManager.Instance.Resolve<ILogger>();
        }

        public void Intercept(IInvocation invocation)
        {
            if (!ShouldIntercept(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
                return;
            }

            if (invocation.Method.IsAsync())
            {
                PerformAsync(invocation);
            }
            else
            {
                PerformSync(invocation);
            }
        }

        private void PerformSync(IInvocation invocation)
        {
            var fullName = invocation.Method.DeclaringType.FullName + "." + invocation.Method.Name;
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                ExceptionHanding(ex, fullName, invocation.Arguments);
            }
        }

        private void PerformAsync(IInvocation invocation)
        {
            var fullName = invocation.Method.DeclaringType.FullName + "." + invocation.Method.Name;
            PerformSync(invocation);

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithFinally(
                    (Task)invocation.ReturnValue,
                    ex =>
                    {
                        ExceptionHanding(ex, fullName, invocation.Arguments);
                    });
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    ex =>
                    {
                        ExceptionHanding(ex, fullName, invocation.Arguments);
                    });
            }
        }

        private void ExceptionHanding(Exception ex, string fullName, object args)
        {
            if (ex != null)
            {
                _logger.Error(ex.Message + $"\r\n{fullName}:\r\n" + args.ToJsonString().Replace("\r\n", " "), ex);
                throw ex;
            }
        }


        private bool ShouldIntercept(MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                return false;
            }

            if (!methodInfo.IsPublic)
            {
                return false;
            }

            if (methodInfo.IsDefined(typeof(RepositoryInterceptorAttribute), true))
            {
                return true;
            }

            //var classType = methodInfo.DeclaringType;
            //if (classType != null)
            //{
            //    if (classType.GetTypeInfo().IsDefined(typeof(JobInterceptorAttribute), true))
            //    {
            //        return true;
            //    }
            //}

            return false;
        }
    }
}
