using HotelManagementSystem.Core.Entities.Common;

namespace HotelManagementSystem.Core.Entities;

public class Service : AuditableEntity
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public Guid ReservationId { get; set; }

    public Reservation? Reservation { get; set; }
}
