using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SqlToMySql.Data;
using SqlToMySql.helpers;
using SqlToMySql.Implementations;

namespace api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
          var serverVersion = new MariaDbServerVersion(new Version(8, 0, 34));
          var connectionString = config.GetConnectionString("SQLConnection");
          services.AddDbContext<ApplicationDbContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion)
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );



            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<DapperContext>();

            services.AddScoped<IGroningen, Groningen>();
            services.AddScoped<IHofuf, Hofuf>();
            services.AddScoped<IJeddah, Jeddah>();
            services.AddScoped<IDapperSQL, DapperSQL>();


            

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            return services;
        }
    }
}
