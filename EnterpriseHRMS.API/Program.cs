using EnterpriseHRMS.API.Configuration;
using EnterpriseHRMS.API.Middleware;
using EnterpriseHRMS.API.Services;
using EnterpriseHRMS.Application.DependencyInjection;
using EnterpriseHRMS.Application.Features.Employee.Mappings;
using EnterpriseHRMS.Application.Services;
using EnterpriseHRMS.Infrastructure.DependencyInjection;
using EnterpriseHRMS.Infrastructure.Persistence.Context;
using EnterpriseHRMS.Infrastructure.Persistence.Seed;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// Serilog
// ---------------------------------------------------------

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/enterprisehrms-.log",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// ---------------------------------------------------------
// Controllers
// ---------------------------------------------------------

builder.Services.AddControllers();

// ---------------------------------------------------------
// Application & Infrastructure
// ---------------------------------------------------------

builder.Services.AddApplication();

builder.Services.AddInfrastructure(
    builder.Configuration);

builder.Services.AddAutoMapper(
    typeof(EmployeeProfile));

// ---------------------------------------------------------
// JWT Configuration
// ---------------------------------------------------------

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

var jwtKey = builder.Configuration["Jwt:Key"];

Log.Information(
    "JWT Key loaded: {KeyLoaded}",
    !string.IsNullOrWhiteSpace(jwtKey));

// ---------------------------------------------------------
// JWT Authentication
// ---------------------------------------------------------

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };
    });

// ---------------------------------------------------------
// Authorization
// ---------------------------------------------------------

builder.Services.AddAuthorization();

// ---------------------------------------------------------
// JWT Service
// ---------------------------------------------------------

builder.Services.AddScoped<IJwtService, JwtService>();

// ---------------------------------------------------------
// Swagger
// ---------------------------------------------------------

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "EnterpriseHRMS API",
            Version = "v1",
            Description =
                "Enterprise HRMS Microservices API"
        });

    // JWT Bearer Authentication
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter your JWT token. Example: Bearer eyJhbGciOi..."
        });

    // Apply Bearer authentication to Swagger endpoints
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});

// ---------------------------------------------------------
// Build Application
// ---------------------------------------------------------

var app = builder.Build();

// ---------------------------------------------------------
// Database Seeding
// ---------------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var context =
        scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

    await DatabaseSeeder.SeedAsync(context);
}

// ---------------------------------------------------------
// Swagger
// ---------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ---------------------------------------------------------
// Middleware Pipeline
// ---------------------------------------------------------

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();