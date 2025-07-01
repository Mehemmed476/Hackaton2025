using HotelManagementSystem.BL.DTOs.Room;
using HotelManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BL.Services.Abstractions;

public interface IRoomService
{
    public Task AddRoomAsync(AddRoomDTO addRoomDTO);
    public Task<ICollection<GetRoomDTO>> GetAllRooms();
    public Task<GetRoomDTO> GetRoomById(Guid Id);
    public Task<GetRoomDTO> GetRoomByCondition(Expression<Func<Room, bool>> expression);
    public Task HardDelete(Guid Id);
    public Task Update(Guid Id, UpdateRoomDTO updateRoomDTO);
    public Task SoftDelete(Guid Id);
    public Task RevertSoftDelete(Guid Id);
    public Task<int> SaveChangesAsync();

}
