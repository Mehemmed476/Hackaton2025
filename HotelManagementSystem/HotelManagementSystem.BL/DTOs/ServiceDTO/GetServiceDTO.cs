using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.Core.Entities.Identity;

namespace HotelManagementSystem.BL.DTOs.ServiceDTO;

public class GetServiceDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public Reservation? Reservation { get; set; }
    public DateTime CreatedAt { get; set; }
    public AppUser CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public AppUser? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public AppUser? DeletedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}
