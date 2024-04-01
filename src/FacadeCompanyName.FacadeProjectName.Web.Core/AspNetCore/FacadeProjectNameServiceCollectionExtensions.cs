using Abp.AspNetCore;
using Abp.Extensions;
using Facade.AspNetCore;
using FacadeCompanyName.FacadeProjectName.Web.Core.Authentication;
using FacadeCompanyName.FacadeProjectName.Web.Core.Filters;
using FacadeCompanyName.FacadeProjectName.Web.Core.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.AspNetCore
{
    public static class FacadeProjectNameServiceCollectionExtensions
    {
        private const string _defaultCorsPolicyName = "localhost";

        /// <summary>
        /// 配置FacadeProjectName服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appConfiguration"></param>
        public static void ConfigureFacadeProjectNameService(this IServiceCollection services, IConfigurationRoot appConfiguration)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            AuthConfigurer.Configure(services, appConfiguration);

            services.Configure<MvcOptions>((op) =>
            {
                // enable debounce or redlock

                op.Filters.AddService(typeof(DebounceActionFilter));
                //op.Filters.AddService(typeof(RedLockActionFilter));
            });

            services.AddSignalR();

            // Configure CORS for angular2 UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );
        }

        /// <summary>
        /// 配置FacadeProjectName服务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="appConfiguration"></param>
        public static void ConfigureFacadeProjectNameApp(this IApplicationBuilder app, IConfigurationRoot appConfiguration)
        {
            app.UseFacade(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = (c) =>
                { // 资源文件跨域
                    c.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                }
            });
            app.UseRouting();
            app.UseCors(_defaultCorsPolicyName); // Enable CORS!
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAbpRequestLocalization();

            // demo middleware
            app.UseMiddleware<DemoMiddleware>();
        }
    }
}
