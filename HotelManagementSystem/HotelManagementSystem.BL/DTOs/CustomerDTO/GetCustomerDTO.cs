using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.Core.Entities.Identity;

namespace HotelManagementSystem.BL.DTOs.CustomerDTO;

public class GetCustomerDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string PhoneNumber { get; set; }
    public ICollection<Reservation>? Reservations { get; set; } = new List<Reservation>();
    public DateTime CreatedAt { get; set; }
    public AppUser CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public AppUser? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public AppUser? DeletedBy { get; set; }
    public bool isDeleted{ get; set; }
}
