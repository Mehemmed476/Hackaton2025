using HotelManagementSystem.Core.Entities.Common;

namespace HotelManagementSystem.Core.Entities;

public class Customer : AuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string PhoneNumber { get; set; }

    public ICollection<Reservation>? Reservations { get; set; } = new List<Reservation>();
}
