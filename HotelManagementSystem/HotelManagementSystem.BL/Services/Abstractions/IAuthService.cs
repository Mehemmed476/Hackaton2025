using HotelManagementSystem.BL.DTOs.AuthDTO;
using HotelManagementSystem.Core.Entities.Identity;

namespace HotelManagementSystem.BL.Services.Abstractions;

public interface IAuthService
{
    public Task<string> LoginAsync(LoginDTO loginDTO);
    public Task RegisterManagerAsync(RegisterDTO registerDTO);
    public Task RegisterUserAsync(RegisterDTO registerDTO);
    public string GenerateToken(AppUser appUser, bool rememberMe);
    public Task<AppUser> GetCurrentUserAsync();

}
