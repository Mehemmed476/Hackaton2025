using HotelManagementSystem.Core.Enums;

namespace HotelManagementSystem.BL.DTOs.Room
{
    public class AddRoomDTO
    {
        public string RoomNumber { get; set; }
        public RoomTypeEnum RoomType { get; set; }
        public decimal PricePerNight { get; set; }

    }
}
