using Microsoft.AspNetCore.Mvc.Filters;




namespace FacadeCompanyName.FacadeProjectName.Web.Core.AspNetCore.Builders
{
    public class TrimStringActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg == null) continue;

                TrimEngine.Trim(arg);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}