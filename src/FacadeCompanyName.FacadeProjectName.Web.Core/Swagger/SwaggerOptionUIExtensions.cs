using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.Swagger
{
    public static class SwaggerOptionUIExtensions
    {
        /// <summary>
        /// 配置 Swagger Options UI
        /// </summary>
        /// <param name="options"></param>
        public static void ConfigureUIFacadeProjectName(this SwaggerUIOptions options)
        {
            options.DisplayRequestDuration();
        }
    }
}
