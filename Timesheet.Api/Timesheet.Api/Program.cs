﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System.Threading.Tasks;

namespace Timesheet.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             .UseSerilog((hostingContext, loggerConfiguration) =>
             {
                 loggerConfiguration
                         .ReadFrom.Configuration(hostingContext.Configuration)
                         .Enrich.FromLogContext();
             })
             .UseStartup<Startup>();
    }
}
