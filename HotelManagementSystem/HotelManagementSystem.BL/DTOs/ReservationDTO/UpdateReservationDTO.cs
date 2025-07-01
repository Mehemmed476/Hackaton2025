namespace HotelManagementSystem.BL.DTOs.ReservationDTO;

public class UpdateResevationDTO
{
    public Guid CustomerId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}
