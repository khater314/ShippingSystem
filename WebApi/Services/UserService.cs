using BL.Contracts;
using BL.DTOs;
using DAL.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Security.Claims;

namespace WebApi.Services
{
    public class UserService(
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, 
        IHttpContextAccessor httpContextAccessor,
        IRefreshTokenRetrevalService refreshToken,
        TokenService tokenService
        ) 
        : BL.Contracts.IUserService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IRefreshTokenRetrevalService _refreshTokenRetreval = refreshToken;
        private readonly TokenService _tokenService = tokenService;

        public async Task<UserResultDto> RegisterAsync(UserRegisterDto registerDto)
        {
            if(registerDto.Password != registerDto.ConfirmedPassword)
            {
                return new UserResultDto
                {
                    IsSuccess = false,
                    Errors = ["Passwords do not match."]
                };
            }
            var user = new AppUser { UserName = registerDto.Email, Email = registerDto.Email };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            return new UserResultDto
            {
                IsSuccess = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserResultDto> LoginAsync(UserLoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent: true, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                return new UserResultDto
                {
                    IsSuccess = false,
                    Errors = ["Invalid login attempt."]
                };
            }
            return new UserResultDto
            {
                IsSuccess = true,
                RefreshToken = _tokenService.GenerateRefreshToken(),
                AccessToken = _tokenService.GenerateAccessToken(
                    [
                        new (ClaimTypes.Name, loginDto.Email),
                        new (ClaimTypes.Role, "User")
                    ])
            };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserReadDto?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return  null;
            }
            return new UserReadDto
            {
                Id = user.Id,
                Email = user.Email!
            };
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .Select(u => new UserReadDto
                {
                    Id = u.Id,
                    Email = u.Email!
                })
                .ToListAsync();
        }

        public async Task<Guid> GetLoggedInUserId()
        {
            var sRefreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["RefreshToken"];
            var oRefreshToken = await _refreshTokenRetreval.GetByToken(sRefreshToken);
            return oRefreshToken.UserId;
        }

        public async Task<IEnumerable<UserReadDto>> GetUsersBySelectedIdsAsync(List<string> ids)
        {
            return await _userManager.Users
                .Where(u => ids.Contains(u.Id.ToString()))
                .Select(u => new UserReadDto
                {
                    Id = u.Id,
                    Email = u.Email!
                })
                .ToListAsync();
        }

        public async Task<UserReadDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) 
                return null;

            return new UserReadDto
            {
                Id = user.Id,
                Email = user.Email!
            };
        }
    }
}
