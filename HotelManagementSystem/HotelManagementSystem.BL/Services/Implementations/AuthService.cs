using HotelManagementSystem.BL.DTOs.AuthDTO;
using HotelManagementSystem.BL.DTOs.RoomDTO;
using HotelManagementSystem.BL.ExternalServices.Abstractions;
using HotelManagementSystem.BL.Services.Abstractions;
using HotelManagementSystem.Core.Entities.Identity;
using HotelManagementSystem.Core.Enums;
using HotelManagementSystem.DL.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagementSystem.BL.Services.Implementations;

public class AuthService : IAuthService
{
    readonly IConfiguration _configuration;
    readonly UserManager<AppUser> _userManager;
    readonly SignInManager<AppUser> _signInManager;
    IHttpContextAccessor _httpContextAccessor;
    IFileUploadService _fileUploadService;
    IWebHostEnvironment _env;
    public AuthService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IFileUploadService fileUploadService, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
        _fileUploadService = fileUploadService;
        _env = env;
    }

    public async Task<AppUser> GetCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        return user;
    }
    public string GenerateToken(AppUser user, bool rememberMe)
    {
        var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = rememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(1);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> LoginAsync(LoginDTO loginDTO)
    {
        AppUser? appUser = await _userManager.FindByEmailAsync(loginDTO.Email);

        if (appUser is null)
        {
            throw new BaseException();
        }
        await _userManager.CheckPasswordAsync(appUser, loginDTO.Password);
     
        var roles = await _userManager.GetRolesAsync(appUser);
        var token = GenerateToken(appUser, loginDTO.RememberMe);
        return token;
    }

    public async Task RegisterUserAsync(RegisterDTO registerDTO)
    {
        AppUser? appUser = await _userManager.FindByNameAsync(registerDTO.FirstName + registerDTO.LastName);

        if (appUser is not null)
        {
            throw new BaseException("this user already exists.");
        }

        AppUser user = new AppUser
        {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            Email = registerDTO.FirstName + registerDTO.LastName + "@gmail.com",
            UserName = registerDTO.FirstName + registerDTO.LastName,
        };
        
        IdentityResult result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            throw new BaseException("Couldn't generate user.");
        }

        IdentityResult roleResult = await _userManager.AddToRoleAsync(user,RoleEnum.User.ToString());
        if (!roleResult.Succeeded)
        {
            throw new BaseException("Couldn't generate role.");
        }

    }

    public async Task RegisterManagerAsync(RegisterDTO registerDTO)
    {
        AppUser? appUser = await _userManager.FindByNameAsync(registerDTO.FirstName + registerDTO.LastName);

        if (appUser is not null)
        {
            throw new BaseException("this user already exists.");
        }

        AppUser user = new AppUser
        {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            Email = registerDTO.FirstName + registerDTO.LastName + "@gmail.com",
            UserName = registerDTO.FirstName + registerDTO.LastName,
            CVImageURL = await _fileUploadService.SaveFileAsync(registerDTO.CVImage, _env.WebRootPath, new[] { ".jpg", ".jpeg", ".webp", ".png" }),
            IdentityCardImageURL = await _fileUploadService.SaveFileAsync(registerDTO.IdentityCardImage, _env.WebRootPath, new[] { ".jpg", ".jpeg", ".webp", ".png" }),
            PersonalImageURL = await _fileUploadService.SaveFileAsync(registerDTO.PersonalImage, _env.WebRootPath, new[] { ".jpg", ".jpeg", ".webp", ".png" })
        };

        IdentityResult result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            throw new BaseException("Couldn't generate user.");
        }

        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, RoleEnum.Manager.ToString());
        if (!roleResult.Succeeded)
        {
            throw new BaseException("Couldn't generate role.");
        }

    }
}
