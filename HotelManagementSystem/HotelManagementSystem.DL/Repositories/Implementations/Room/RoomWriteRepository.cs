using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.DL.Contexts;
using HotelManagementSystem.DL.Repositories.Abstractions;

namespace HotelManagementSystem.DL.Repositories.Implementations;

public class RoomWriteRepository : WriteRepository<Room>, IRoomWriteRepository
{
    public RoomWriteRepository(AppDbContext context) : base(context)
    {
    }
}
