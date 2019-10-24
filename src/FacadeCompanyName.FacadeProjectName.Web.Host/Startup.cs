using Abp.AspNetCore;
using Abp.Extensions;
using Abp.Reflection.Extensions;
using Castle.Facilities.Logging;
using Facade.AspNetCore;
using Facade.AspNetCore.Configuration;
using Facade.AspNetCore.Web.NLog;
using Facade.Castle.NLogger;
using Facade.Core.Configuration;
using FacadeCompanyName.FacadeProjectName.Web.Host.Authentication;
using FacadeCompanyName.FacadeProjectName.Web.Host.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;

namespace FacadeCompanyName.FacadeProjectName.Web.Host
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.BuildConfiguration(new FacadeConfigurationBuilderOptions()
            {
                UserSecretsAssembly = typeof(FacadeProjectNameWebHostModule).GetAssembly()
            });
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Latest).AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            //IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            services.AddSignalR();

            // Configure CORS for angular2 UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            _appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );

            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "FacadeProjectName API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            // Configure Abp and Dependency Injection
            return services.AddFacade<FacadeProjectNameWebHostModule>(
                // Configure Log4Net logging
                options =>
                {
                    options.IocManager.IocContainer.AddFacility<LoggingFacility>(f =>
                    {
                        f.UseFacadeNLog();
                        //使用Facade.NLogger.Exceptionless
                        //f.UseFacadeNLog(_appConfiguration["Exceptionless:ServerUrl"], _appConfiguration["Exceptionless:ApiKey"]);
                    });
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseFacade(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(_defaultCorsPolicyName); // Enable CORS!
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAbpRequestLocalization();

            app.UseEndpoints(c =>
            {
                c.MapHub<FacadeProjectNameHub>("/signalr");
                c.MapControllers();
            });


            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(_appConfiguration["App:ServerRootAddress"].EnsureEndsWith('/') + "swagger/v1/swagger.json", "FacadeProjectName API V1");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("FacadeCompanyName.FacadeProjectName.Web.Host.wwwroot.swagger.ui.index.html");
            }); // URL: /swagger
        }
    }
}
