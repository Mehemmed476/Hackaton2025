using HotelManagementSystem.Core.Enums;

namespace HotelManagementSystem.BL.DTOs.RoomDTO;

public class UpdateRoomDTO
{
    public string RoomNumber { get; set; }
    public RoomTypeEnum RoomType { get; set; }
    public decimal PricePerNight { get; set; }
    public RoomStatusEnum RoomStatus { get; set; }


}
