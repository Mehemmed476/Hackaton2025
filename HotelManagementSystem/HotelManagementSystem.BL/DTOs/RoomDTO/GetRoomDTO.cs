using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.Core.Entities.Identity;
using HotelManagementSystem.Core.Enums;

namespace HotelManagementSystem.BL.DTOs.RoomDTO;

public class GetRoomDTO
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public AppUser CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public AppUser? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public AppUser? DeletedBy { get; set; }
    public string RoomNumber { get; set; }
    public RoomTypeEnum RoomType { get; set; }
    public decimal PricePerNight { get; set; }
    public RoomStatusEnum RoomStatus { get; set; }
    public string RoomImageURL { get; set; }
    public ICollection<Reservation>? Reservations { get; set; }
}
