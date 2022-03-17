using System;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UowLockInterceptorAttribute : Attribute
    {
    }
}
