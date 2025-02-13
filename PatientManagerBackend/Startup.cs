using PatientManagerBackend.Infrastructure.Extension;
using Serilog;

namespace PatientManagerBackend
{
    public class Startup
    {
        private readonly IConfigurationRoot configRoot;

        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            configRoot = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScopedServices();

            services.AddDatabaseContext(Configuration);

            services.AddCustomAutoMapper();

            services.AddSwaggerOpenAPI();

            services.AddCustomOptions();

            services.AddCustomControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            app.ConfigureCors(Configuration);

            app.ConfigureCustomExceptionMiddleware();

            app.ConfigureSwagger(env);

            log.AddSerilog();

            app.UseRouting();

            //app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
