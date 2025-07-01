using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.DL.Contexts;
using HotelManagementSystem.DL.Repositories.Abstractions;

namespace HotelManagementSystem.DL.Repositories.Implementations;

public class ReservationReadRepository : ReadRepository<Reservation>, IReservationReadRepository
{
    public ReservationReadRepository(AppDbContext context) : base(context)
    {
    }
}
