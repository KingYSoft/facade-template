using Abp.Dependency;
using Abp.Json;
using Abp.Threading;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share.Interceptors
{
    public class DapperRepositoryInterceptor : IInterceptor
    {
        private readonly ILogger _logger;

        public DapperRepositoryInterceptor()
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

        private void ExceptionHanding(Exception ex, string fullName, object[] args)
        {
            if (ex != null)
            {
                var msg = string.Empty;
                if (args != null)
                {
                    var s = args.ToList<object>();
                    if (s.Any())
                    {
                        if (s[0] is string)
                        {
                            msg = "  " + s[0] as string;
                            s.RemoveAt(0);
                            if (s.Any())
                            {
                                msg += Environment.NewLine;
                                msg += "  执行sql语句参数：" + s.ToJsonString();
                            }
                        }
                    }
                }

                _logger.Error($"{ex.Message}{Environment.NewLine}  {fullName}，执行sql语句失败：{Environment.NewLine}" + msg);
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

            if (methodInfo.IsDefined(typeof(DapperRepositoryInterceptorAttribute), true))
            {
                return true;
            }

            return false;
        }
    }
}
