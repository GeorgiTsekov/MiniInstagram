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
    }
}