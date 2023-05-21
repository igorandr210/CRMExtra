using System.Linq;
using System.Threading.Tasks;
using Application.Factories;
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await UpdateQuartzJobs(host);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static async Task UpdateQuartzJobs(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var environment = services.GetRequiredService<IHostEnvironment>();
            if (environment.IsDevelopment() || environment.IsEnvironment("Local"))
                return;
                
            var applicationDbContext = services.GetRequiredService<IApplicationDbContext>();
            var automationCronSettings = await applicationDbContext.JobCronSettings.Where(r => r.Status).ToListAsync();

            var factory = services.GetRequiredService<JobKeyFactory>();
            var jobFactory = services.GetRequiredService<ISchedulerFactory>();
            var scheduler = await jobFactory.GetScheduler();
            foreach (var automationCronSetting in automationCronSettings)
            {
                var job = factory.Invoke(automationCronSetting.JobType);
                if (job != null)
                {
                    var jD = await scheduler.GetJobDetail(job);
                    await scheduler.DeleteJob(job);
                    await scheduler.ScheduleJob(jD, TriggerBuilder.Create().WithCronSchedule(automationCronSetting.CronSettingString, x => x.WithMisfireHandlingInstructionIgnoreMisfires()).ForJob(job).Build());
                }
            }
        }
    }
}
