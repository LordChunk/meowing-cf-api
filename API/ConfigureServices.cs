﻿using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public static class ConfigureServices
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationContext>(o => o.UseMySQL(config["MySQLConnectionStrings:Default"]));
        }

        // Repository wrapper is instantiated as singleton to prevent it from checking whether the database is created on each request
        public static void ConfigureRepositoryWrapper(this IServiceCollection services) => services.AddScoped<RepositoryWrapper>();
    }
}