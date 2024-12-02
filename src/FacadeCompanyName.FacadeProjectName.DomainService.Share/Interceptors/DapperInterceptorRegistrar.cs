using Castle.Core;
using Castle.MicroKernel;
using System;
using System.Linq;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share.Interceptors
{
    public class DapperInterceptorRegistrar
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (JobShouldIntercept(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(DapperRepositoryInterceptor)));
            }


        }
        private static bool JobShouldIntercept(Type type)
        {

            if (type.GetMethods().Any(m => m.IsDefined(typeof(DapperRepositoryInterceptorAttribute), true)))
            {
                return true;
            }

            return false;
        }
    }
}
