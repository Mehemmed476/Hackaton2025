using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.DL.Contexts;
using HotelManagementSystem.DL.Repositories.Abstractions;

namespace HotelManagementSystem.DL.Repositories.Implementations;

public class ReservationWriteRepository : WriteRepository<Reservation>, IReservationWriteRepository
{
    public ReservationWriteRepository(AppDbContext context) : base(context)
    {
    }
}
