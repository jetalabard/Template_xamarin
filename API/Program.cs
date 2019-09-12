using System;
using System.Threading;
using API.Models.Mock;
using Entities.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MZP.WS;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int processorCounter = Environment.ProcessorCount;
            bool success = ThreadPool.SetMaxThreads(processorCounter, processorCounter);
            if (success)
            {
                IWebHost host = CreateWebHostBuilder(args).Build();

                using (IServiceScope scope = host.Services.CreateScope())
                {
                    // 3. Get the instance of BoardGamesDBContext in our services layer
                    IServiceProvider services = scope.ServiceProvider;
                    TemplateContext context = services.GetRequiredService<TemplateContext>();
                    if (context != null && context.Database.IsInMemory())
                    {
                        // 4. Call the DataGenerator to create sample data
                        DataGenerator.Initialize(services);
                    }
                }

                // Continue to run the application
                host.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
