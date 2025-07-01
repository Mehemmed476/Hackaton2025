using HotelManagementSystem.BL.DTOs.ServiceDTO;
using HotelManagementSystem.Core.Entities;
using System.Linq.Expressions;

namespace HotelManagementSystem.BL.Services.Abstractions;

public interface IServiceService
{
    public Task AddServiceAsync(AddServiceDTO addServiceDTO);
    public Task<ICollection<GetServiceDTO>> GetAllServices();
    public Task<GetServiceDTO> GetServiceById(Guid Id);
    public Task<GetServiceDTO> GetServiceByCondition(Expression<Func<Service, bool>> expression);
    public Task HardDelete(Guid Id);
    public Task Update(Guid Id, UpdateServiceDTO updateServiceDTO);
    public Task SoftDelete(Guid Id);
    public Task RevertSoftDelete(Guid Id);
    public Task<int> SaveChangesAsync();

}
