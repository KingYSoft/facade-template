using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FacadeCompanyName.FacadeProjectName.Web.Host
{
    public class Program
    {
        private const string urls = "server.urls";
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();
            string url = config[urls] ?? "http://*:21021";

            return WebHost.CreateDefaultBuilder(args)
                  .UseUrls(url)
                  .UseStartup<Startup>()
                  .UseKestrel(options =>
                  {
                      options.Limits.MaxRequestBodySize = null;
                      options.Limits.MaxRequestBufferSize = null;
                  });
        }
    }
}
