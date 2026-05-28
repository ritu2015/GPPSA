using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using GPPSA.Api.Validators;
using GPPSA.Application.Interfaces;
using GPPSA.Application.Mappings;
using GPPSA.Application.Services;
using GPPSA.Infrastructure.Data;
using GPPSA.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GPPSA.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
               public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b=> b.MigrationsAssembly("GPPSA.Infrastructure")
                    )
                );

            // Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Services
            services.AddScoped<EmployeeService>();

            // FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeDtoValidator>();

            // AutoMapper
            services.AddAutoMapper(typeof(EmployeeProfile));

           

            // Validation → ProblemDetails
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var problemDetails = new ValidationProblemDetails(errors)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation Failed",
                        Detail = "See the errors property for details.",
                        Instance = context.HttpContext.Request.Path
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            var jwtSettings = configuration.GetSection("JwtSettings");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!))
                };
            });

            // Authorization policies
            services.AddAuthorizationBuilder()
                .AddPolicy("AuthenticatedUser", policy =>
                    policy.RequireAuthenticatedUser());

            services.AddScoped<JwtTokenService>();
            // Add CORS
services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
        policy.WithOrigins("http://localhost:4200","https://gppsa-ui-cubsbvcyhbgmchf4.canadacentral-01.azurewebsites.net")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

            return services;
        }
    }
}