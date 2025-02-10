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
