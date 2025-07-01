using HotelManagementSystem.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace HotelManagementSystem.BL.DTOs.RoomDTO;

public class AddRoomDTO
{
    public string RoomNumber { get; set; }
    public RoomTypeEnum RoomType { get; set; }
    public decimal PricePerNight { get; set; }
    public IFormFile Image { get; set; }

}
