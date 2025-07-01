using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.DL.Contexts;
using HotelManagementSystem.DL.Repositories.Abstractions;

namespace HotelManagementSystem.DL.Repositories.Implementations;

public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(AppDbContext context) : base(context)
    {
    }
}
