using HotelManagementSystem.DL.Contexts;
using HotelManagementSystem.DL.Repositories.Abstractions;
using HotelManagementSystem.DL.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagementSystem.DL;

public static class DLRegistration
{
    public static void AddDlServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("MsSql"));
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        service.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        service.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

        service.AddScoped<IReservationReadRepository, ReservationReadRepository>();
        service.AddScoped<IReservationWriteRepository, ReservationWriteRepository>();

        service.AddScoped<IRoomReadRepository, RoomReadRepository>();
        service.AddScoped<IRoomWriteRepository, RoomWriteRepository>();

        service.AddScoped<IServiceReadRepository, ServiceReadRepository>();
        service.AddScoped<IServiceWriteRepository, ServiceWriteRepository>();

    }
}
