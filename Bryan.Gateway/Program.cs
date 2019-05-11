using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bryan.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((hostingContext, builder) =>
                 {
                     builder
                     .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                     .AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
                 })
                .UseUrls("http://*:5009")
                .UseStartup<Startup>();
    }
}
