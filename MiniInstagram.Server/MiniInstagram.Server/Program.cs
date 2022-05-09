using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniInstagram.Server.Data;
using MiniInstagram.Server.Data.Models;

var builder = WebApplication.CreateBuilder(args);
    
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MiniInstagramDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MiniInstagramDbContext>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();    
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
