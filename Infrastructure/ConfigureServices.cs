using Amazon.Runtime;
using Application.AssignmentTasks.DTOs;
using Application.Common.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.ProfilesData.Dto;
using Domain.Entities;
using Infrastructure.AutoMapper.FileStorage;
using Infrastructure.Configurations;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IIntakeFormRepository, IntakeFormRepository>();
            services.AddScoped(typeof(IBaseRepository<ProfileData>), typeof(ProfileDataRepository));
            services.AddScoped(typeof(IBaseRepository<AssignmentTask>),
                typeof(AssignmentTaskRepository));
            services.AddScoped(typeof(IPaginatedRepository<ProfileData, PaginationInfoProfileDto>), typeof(ProfileDataRepository));
            services.AddScoped(typeof(IPaginatedRepository<AssignmentTask, PaginationInfoTaskDto>),
                typeof(AssignmentTaskRepository));
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(FileStorageProfile));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddRepositories();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IStorageService, EnvelopeStorageService>();
            services.AddTransient<IStorageService, DocumentsStorageService>();
            services.AddTransient<IDocuSignService, DocuSignService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient(typeof(IFileExportService<>), typeof(FileExportService<>));

            services.Configure<CognitoConfiguration>(options => configuration.GetSection(CognitoConfiguration.ConfigurationSection).Bind(options));
            services.Configure<DocuSignConfiguration>(options => configuration.GetSection(DocuSignConfiguration.ConfigurationSection).Bind(options));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            #region AddAwsConfig

            var awsOptions = configuration.GetAWSOptions();

            awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();
            services.AddDefaultAWSOptions(awsOptions);

            #endregion

            return services;
        }
    }
}
