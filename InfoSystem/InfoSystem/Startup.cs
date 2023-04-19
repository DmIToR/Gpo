using System.Security.Claims;
using System.Text;
using InfoSystem.Data;
using InfoSystem.Entities;
using InfoSystem.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration.GetValue<string>("Jwt:Issuer"),
                    ValidateAudience = true,
                    ValidAudience = _configuration.GetValue<string>("Jwt:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            _configuration.GetValue<string>("Jwt:SecretKey")
                        )
                    ),
                    ValidateLifetime = true
                };
            });

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
            
            options.AddPolicy("EducationDepartment", config =>
            {
                config.RequireClaim(ClaimTypes.Role, "EducationDepartment");
            });
            
            options.AddPolicy("Teacher", config =>
            {
                config.RequireClaim(ClaimTypes.Role, "Teacher");
            });
            
            options.AddPolicy("Secretary", config =>
            {
                config.RequireClaim(ClaimTypes.Role, "Secretary");
            });
        });
        
        const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

        services.AddCors(options =>
        {
            options.AddPolicy(name:myAllowSpecificOrigins, 
                builder =>
                {
                    builder.WithOrigins("http://localhost",
                            "http://localhost:3000",
                            "http://localhost:5299")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowedToAllowWildcardSubdomains();
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
        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors("_myAllowSpecificOrigins");
        app.UseAuthorization();

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