using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.DL.Contexts;
using HotelManagementSystem.DL.Repositories.Abstractions;

namespace HotelManagementSystem.DL.Repositories.Implementations;

public class RoomReadRepository : ReadRepository<Room>, IRoomReadRepository
{
    public RoomReadRepository(AppDbContext context) : base(context)
    {
    }
}
