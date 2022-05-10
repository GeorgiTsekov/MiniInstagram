using Microsoft.EntityFrameworkCore;
using MiniInstagram.Server.Data;

namespace MiniInstagram.Server.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<MiniInstagramDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
