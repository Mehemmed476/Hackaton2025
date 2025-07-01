namespace HotelManagementSystem.BL.DTOs.ServiceDTO;

public class UpdateServiceDTO
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public Guid ReservationId { get; set; }
}
