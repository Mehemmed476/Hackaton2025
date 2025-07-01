using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.DL.Contexts;
using HotelManagementSystem.DL.Repositories.Abstractions;

namespace HotelManagementSystem.DL.Repositories.Implementations;

public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(AppDbContext context) : base(context)
    {
    }
}
