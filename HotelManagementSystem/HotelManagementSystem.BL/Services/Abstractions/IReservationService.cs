using HotelManagementSystem.BL.DTOs.ReservationDTO;
using HotelManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BL.Services.Abstractions;

public interface IReservationService
{
    public Task AddReservationAsync(AddReservationDTO addReservationDTO);
    public Task<ICollection<GetReservationDTO>> GetAllReservations();
    public Task<GetReservationDTO> GetReservationById(Guid Id);
    public Task<GetReservationDTO> GetReservationByCondition(Expression<Func<Reservation, bool>> expression);
    public Task HardDelete(Guid Id);
    public Task Update(Guid Id, UpdateReservationDTO updateReservationDTO);
    public Task SoftDelete(Guid Id);
    public Task RevertSoftDelete(Guid Id);
    public Task<int> SaveChangesAsync();
}
