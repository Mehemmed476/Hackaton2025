using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.DL.Contexts;
using HotelManagementSystem.DL.Repositories.Abstractions;

namespace HotelManagementSystem.DL.Repositories.Implementations;

public class ServiceReadRepository : ReadRepository<Service>, IServiceReadRepository
{
    public ServiceReadRepository(AppDbContext context) : base(context)
    {
    }
}
