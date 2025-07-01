using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.Core.Enums;

namespace HotelManagementSystem.BL.DTOs.Room
{
    public class GetRoomDTO
    {
        public string RoomNumber { get; set; }
        public RoomTypeEnum RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public RoomStatusEnum RoomStatus { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
