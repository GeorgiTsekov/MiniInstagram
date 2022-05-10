using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniInstagram.Server;
using MiniInstagram.Server.Data;
using MiniInstagram.Server.Data.Models;
using MiniInstagram.Server.Infrastructure.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
    
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var services = builder.Services;

services.AddDbContext<MiniInstagramDbContext>(options => options.UseSqlServer(connectionString));

//services.AddDatabaseDeveloperPageExceptionFilter();

services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<MiniInstagramDbContext>();

var appSettingsConfiguration = builder.Configuration.GetSection("ApplicationSettings");
services.Configure<AppSettings>(appSettingsConfiguration);

var appSettings = appSettingsConfiguration.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

services.AddControllers();

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// global cors policy
app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// custom jwt auth middleware
//app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

app.ApplyMigrations();
