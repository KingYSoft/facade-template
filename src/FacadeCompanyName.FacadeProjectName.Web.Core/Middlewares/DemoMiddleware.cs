using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.Middlewares
{
    public class DemoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public DemoMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // _logger.Debug("DemoMiddleware");
            await _next(httpContext);
        }
    }
}
