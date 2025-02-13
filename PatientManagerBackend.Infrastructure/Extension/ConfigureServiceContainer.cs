using AspNetCoreRateLimit;
using PatientManagerBackend.Core.Contract;
using PatientManagerBackend.Core.Contract.Repository;
using PatientManagerBackend.Core.Implementation;
using PatientManagerBackend.Core.Repository;
using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.Settings;
using PatientManagerBackend.Infrastructure.Configs;
using PatientManagerBackend.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Polly;
using Polly.Extensions.Http;
using System.Net;
using System.Text;

namespace PatientManagerBackend.Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        public static void AddDatabaseContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var databaseOptions = new DatabaseOptions();
            var databaseSection = configuration.GetSection("DatabaseOptions");
            databaseSection.Bind(databaseOptions);

            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(databaseOptions.ConnectionString, b =>
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });
            });
        }


        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            serviceCollection.AddScoped<IPatientService, PatientService>();
            serviceCollection.AddScoped<IPatientRecordService, PatientRecordService>();


            serviceCollection.AddScoped<IPatientRepository, PatientRepository>();
            serviceCollection.AddScoped<IPatientRecordRepository, PatientRecordRepository>();

        }


        
        public static void AddCustomOptions(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddOptions<AdminOptions>().BindConfiguration("AdminOptions");
            serviceCollection.AddOptions<DatabaseOptions>().BindConfiguration("DatabaseOptions");
        }
              
        public static void AddCustomAutoMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(MappingProfileConfiguration));
        }

        public static void AddSwaggerOpenAPI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(setupAction =>
            {

                setupAction.SwaggerDoc(
                    "OpenAPISpecification",
                    new OpenApiInfo()
                    {
                        Title = "Patient Manager WebAPI",
                        Version = "1",
                        Description = "API Details for PatientManagerBackend Admin",
                        Contact = new OpenApiContact()
                        {
                            Email = "info@thePatientManagerBackend.com",
                            Name = "Patient Manager",
                            Url = new Uri(" https://patientmanager.com")
                        },
                        License = new OpenApiLicense()
                        {
                            Name = "UNLICENSED"
                        }
                    });

                //setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.Http,
                //    Scheme = "bearer",
                //    BearerFormat = "JWT",
                //    Description = $"Input your Bearer token in this format - Bearer token to access this API",
                //});
                //setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer",
                //            },
                //        }, new List<string>()
                //    },
                //});
            });
        }

        public static void AddCustomControllers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddEndpointsApiExplorer();

            serviceCollection.AddControllersWithViews()
                .AddNewtonsoftJson(ops =>
                {
                    ops.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    ops.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                });
            serviceCollection.AddRazorPages();

            serviceCollection.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
                apiBehaviorOptions.InvalidModelStateResponseFactory = actionContext =>
                {
                    var logger = actionContext.HttpContext.RequestServices.GetRequiredService<ILogger<BadRequestObjectResult>>();
                    IEnumerable<string> errorList = actionContext.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                    logger.LogError("Bad Request");
                    logger.LogError(string.Join(",", errorList));
                    return new BadRequestObjectResult(new Domain.Common.Response<IEnumerable<string>>("PatientManagerBackend Validation Error", 400, errorList));
                });
        }

    }
}
