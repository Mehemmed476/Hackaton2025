using Microsoft.AspNetCore.Http;

namespace HotelManagementSystem.BL.DTOs.AuthDTO;

public class RegisterDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public IFormFile PersonalImage { get; set; }
    public IFormFile CVImage { get; set; }
    public IFormFile IdentityCardImage { get; set; }
}