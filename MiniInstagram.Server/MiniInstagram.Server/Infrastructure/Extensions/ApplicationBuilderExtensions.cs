using Microsoft.EntityFrameworkCore;
using MiniInstagram.Server.Data;

namespace MiniInstagram.Server.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My MiniInstagram API");
                    options.RoutePrefix = String.Empty;
                });

            return app;
        }

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<MiniInstagramDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
