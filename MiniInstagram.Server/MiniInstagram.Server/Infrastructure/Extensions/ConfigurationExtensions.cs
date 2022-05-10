namespace MiniInstagram.Server.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnection(this ConfigurationManager configuration)
        {
            return configuration.GetConnectionString("DefaultConnection");
        }

        public static AppSettings GetAppSettings(this ConfigurationManager configuration, IServiceCollection services)
        {
            var appSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(appSettingsConfiguration);

            return appSettingsConfiguration.Get<AppSettings>();
        }
    }
}
