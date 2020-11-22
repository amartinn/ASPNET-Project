﻿namespace CasesNET.Web.Infrastructure.Extensions
{
    using CasesNET.Data;
    using CasesNET.Data.Common;
    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Data.Repositories;
    using CasesNET.Services.Data;
    using CasesNET.Services.Messaging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(
         this IServiceCollection services,
         IConfiguration configuration)
         => services
             .AddDbContext<ApplicationDbContext>(options => options
                 .UseSqlServer(configuration.GetDefaultConnectionString()));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddSingleton(configuration)
            .AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>))
            .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
            .AddScoped<IDbQueryRunner, DbQueryRunner>()
            .AddTransient<IEmailSender, NullMessageSender>()
            .AddTransient<ISettingsService, SettingsService>();

        public static IServiceCollection ApplyControllerRules(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            return services;
        }

        public static IServiceCollection ConfigureCookies(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(
             options =>
             {
                 options.CheckConsentNeeded = context => true;
                 options.MinimumSameSitePolicy = SameSiteMode.None;
             });
            return services;
        }
    }
}
