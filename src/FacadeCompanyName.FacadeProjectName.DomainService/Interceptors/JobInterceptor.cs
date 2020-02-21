using Abp.Dependency;
using Abp.Threading;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Interceptors
{
    public class JobInterceptor : IInterceptor
    {
        private readonly ILogger _logger;

        public JobInterceptor()
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
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                ExceptionHanding(ex);
            }
        }

        private void PerformAsync(IInvocation invocation)
        {
            PerformSync(invocation);

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithFinally(
                    (Task)invocation.ReturnValue,
                    ex =>
                    {
                        ExceptionHanding(ex);
                    });
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    ex =>
                    {
                        ExceptionHanding(ex);
                    });
            }
        }

        private void ExceptionHanding(Exception ex)
        {
            if (ex != null)
            {
                _logger.Error(ex.Message, ex);
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

            if (methodInfo.IsDefined(typeof(JobInterceptorAttribute), true))
            {
                return true;
            }

            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
                if (classType.GetTypeInfo().IsDefined(typeof(JobInterceptorAttribute), true))
                {
                    return true;
                }
            }

            return false;
        }
    }
}


