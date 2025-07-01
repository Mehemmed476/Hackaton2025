using HotelManagementSystem.Core.Entities.Identity;

namespace HotelManagementSystem.BL.DTOs.Reservation;

public class GetResevationDTO
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public AppUser CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public AppUser? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public AppUser? DeletedBy { get; set; }
    public Guid CustomerId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}
