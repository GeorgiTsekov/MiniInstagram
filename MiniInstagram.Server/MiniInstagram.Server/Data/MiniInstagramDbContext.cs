using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniInstagram.Server.Data.Models;

namespace MiniInstagram.Server.Data
{
    public class MiniInstagramDbContext : IdentityDbContext<User>
    {
        public MiniInstagramDbContext(DbContextOptions<MiniInstagramDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Game>()
                .HasOne(g => g.User)
                .WithMany(u => u.Games)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}