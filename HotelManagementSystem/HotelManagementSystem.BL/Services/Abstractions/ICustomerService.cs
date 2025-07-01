using HotelManagementSystem.BL.DTOs.CustomerDTO;
using HotelManagementSystem.Core.Entities;
using System.Linq.Expressions;

namespace HotelManagementSystem.BL.Customers.Abstractions;

public interface ICustomerService
{
    public Task AddCustomerAsync(AddCustomerDTO addCustomerDTO);
    public Task<ICollection<GetCustomerDTO>> GetAllCustomers();
    public Task<GetCustomerDTO> GetCustomerById(Guid Id);
    public Task<GetCustomerDTO> GetCustomerByCondition(Expression<Func<Customer, bool>> expression);
    public Task HardDelete(Guid Id);
    public Task Update(Guid Id, UpdateCustomerDTO updateCustomerDTO);
    public Task SoftDelete(Guid Id);
    public Task RevertSoftDelete(Guid Id);
    public Task<int> SaveChangesAsync();
}
