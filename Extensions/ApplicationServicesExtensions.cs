using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SqlToMySql.Data;
using SqlToMySql.helpers;

namespace api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            /*   services.AddDbContext<DataContext>(options => {
                   options.UseSqlite(config.GetConnectionString("DefaultConnection"));
               }); */


            var serverVersion = new MariaDbServerVersion(new Version(8, 0, 34));
            var connectionString = config.GetConnectionString("MySQLConnection");
            
            services.AddDbContext<ApplicationDbContext>(dbContextOptions =>
                dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    // The following three options help with debugging, but should
                    // be changed or removed for production.
                    .LogTo(Console.WriteLine, LogLevel.Information)
            //.EnableSensitiveDataLogging()
            //.EnableDetailedErrors()
            );

            /*     var _connectionString = config.GetConnectionString("MySQLConnection");
                services.AddDbContext<DapperContext>(
                    options => options.UseMySql(
                        _connectionString,
                        ServerVersion.AutoDetect(_connectionString)
                    )
                );  */


            // services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            //services.Configure<ComSettings>(config.GetSection("ComSettings"));

            /*          services.AddDbContext<DataContext>(options => options
                       .UseMySql(config.GetConnectionString("SQLConnection"),
                           mysqlOptions =>
                               mysqlOptions.ServerVersion(new Pomelo.EntityFrameworkCore.MySql.Storage.ServerVersion(new Version(10, 4, 6), ServerType.MariaDb)).EnableRetryOnFailure()));
        */



            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<DapperContext>();

            /*  services.AddScoped<ITokenService, TokenService>();
             services.AddScoped<IUserRepository, UserRepository>();
 
             services.AddScoped<IEmployeeRepository, EmployeeRepository>();
             services.AddScoped<IValveRepository, ValveRepository>();
             services.AddScoped<IProcedureRepository, ProcedureRepository>();
             services.AddScoped<IPatientRepository, PatientRepository>();
             services.AddScoped<ILtxRepository, LtxRepository>();
 
             services.AddScoped<IAorticSurgery, AorticSurgery>();
             services.AddScoped<IMinInv, MinInv>();
             services.AddScoped<ICABGRepository, CABGRepository>();
             services.AddScoped<ICPBRepository, CPBRepository>();
             services.AddScoped<IPORepository, PORepository>();
             services.AddScoped<IDischarge, Discharge>();
             services.AddScoped<IMinInv, MinInv>();
             services.AddScoped<IRefPhys, RefPhys>();
             services.AddScoped<IUserOnline, UserOnline>();
            
 
             services.AddScoped<IStatistics, Statistics>();
             services.AddScoped<IElementaryStatistics, ElementaryStatistics>();
           
             services.AddScoped<OperatieDrops>();
             services.AddScoped<SpecialMaps>();
             services.AddScoped<LogUserActivity>();
            
             services.AddTransient<IUsers, Users>(); */


            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            return services;
        }
    }
}
