using Castle.Core;
using Castle.MicroKernel;
using System;
using System.Linq;

namespace FacadeCompanyName.FacadeProjectName.Oracle.Interceptors
{
    public class InterceptorRegistrar
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (JobShouldIntercept(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(RepositoryInterceptor)));
            }


        }
        private static bool JobShouldIntercept(Type type)
        {

            //if (type.GetTypeInfo().IsDefined(typeof(JobInterceptorAttribute), true))
            //{
            //    return true;
            //}

            if (type.GetMethods().Any(m => m.IsDefined(typeof(RepositoryInterceptorAttribute), true)))
            {
                return true;
            }

            return false;
        }
    }
}
