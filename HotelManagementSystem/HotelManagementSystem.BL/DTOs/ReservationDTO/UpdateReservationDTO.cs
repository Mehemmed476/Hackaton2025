namespace HotelManagementSystem.BL.DTOs.ReservationDTO;

public class UpdateReservationDTO
{
    public Guid CustomerId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}
