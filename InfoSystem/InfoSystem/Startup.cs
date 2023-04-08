using System.Security.Claims;
using InfoSystem.Data;
using InfoSystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InfoSystem;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("Default"));
            })
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", config =>
            {
                config.RequireClaim(ClaimTypes.Role, "Admin");
            });
            
            options.AddPolicy("Student", config =>
            {
                config.RequireClaim(ClaimTypes.Role, "Student");
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}