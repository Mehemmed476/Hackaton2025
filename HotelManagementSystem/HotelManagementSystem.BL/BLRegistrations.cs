using FluentValidation;
using FluentValidation.AspNetCore;
using HotelManagementSystem.BL.Customers.Abstractions;
using HotelManagementSystem.BL.Customers.Implementations;
using HotelManagementSystem.BL.ExternalServices.Abstractions;
using HotelManagementSystem.BL.ExternalServices.Implementations;
using HotelManagementSystem.BL.Profiles;
using HotelManagementSystem.BL.Services.Abstractions;
using HotelManagementSystem.BL.Services.Implementations;
using HotelManagementSystem.Core.Entities.Identity;
using HotelManagementSystem.DL.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace HotelManagementSystem.BL;

public static class BLRegistrations
{
    public static void AddBlServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        service.AddIdentity<AppUser, IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();

        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });

        service.AddAuthorization();

        service.AddAutoMapper(typeof(RoomProfile).Assembly);
        service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        service.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        service.AddScoped<ICustomerService, CustomerService>();
        service.AddScoped<IReservationService, ReservationService>();
        service.AddScoped<IRoomService, RoomService>();
        service.AddScoped<IServiceService, ServiceService>();
        service.AddScoped<IAuthService, AuthService>();

        service.AddScoped<IFileUploadService, FileUploadService>();
    }
}
