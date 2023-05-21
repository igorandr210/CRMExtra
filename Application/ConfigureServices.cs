using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Polly;
using System.Net.Http;
using System;
using System.Linq;
using Application.AutoMapper.AuthorizationMapper;
using Application.Common.Behaviours;
using static Application.Common.Factories.RestCllientFactory;
using Application.Common.Factories;
using Application.Factories;
using Application.Jobs;
using Domain.Enum;
using Quartz;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AuthorizationAutoMapper));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AccessBehaviour<,>));
            services.AddTransient<FileServiceFactory>();
            services.AddTransient(sp => sp.GetRequiredService<FileServiceFactory>().GetFileServiceDeligate());
            services.RegisterAccessValidators();
            services.AddTransient(sp => GetClientDeligate((client, type) =>
            {
                switch (type)
                {
                    default:
                        client.SetPollicy(Policy
                            .Handle<HttpRequestException>()
                            .WaitAndRetryAsync(1, i => TimeSpan.FromSeconds(30)));
                        break;
                }
            }));
            
            services
                .AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionJobFactory();
                    q.AddJob<UnsignedNotificationJob>(config =>
                    {
                        config
                            .WithIdentity(nameof(UnsignedNotificationJob))
                            .StoreDurably();
                    });
                })
                .AddQuartzHostedService();
            
            services.AddScoped<JobKeyFactory>(x => jobType =>
            {
                var jobName = jobType switch
                {
                    JobType.ReminderJob => nameof(UnsignedNotificationJob),
                    _ => throw new ArgumentOutOfRangeException(nameof(jobType), jobType, null)
                };

                return new JobKey(jobName);
            });

            return services;
        }

        private static IServiceCollection RegisterAccessValidators(this IServiceCollection services)
        {
            var repositoryTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsInterface &&  
                                                                 x.GetInterface(typeof(IAccessValidator<,>).Name) != null);
            foreach (var repositoryType in repositoryTypes)
            {
                var type = repositoryType.UnderlyingSystemType;
                services.AddTransient(type.GetInterface(typeof(IAccessValidator<,>).Name), type);
            }

            return services;
        } 
    }
}
