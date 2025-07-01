using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.DL.Contexts;
using HotelManagementSystem.DL.Repositories.Abstractions;

namespace HotelManagementSystem.DL.Repositories.Implementations;

public class ServiceWriteRepository : WriteRepository<Service>, IServiceWriteRepository
{
    public ServiceWriteRepository(AppDbContext context) : base(context)
    {
    }
}
