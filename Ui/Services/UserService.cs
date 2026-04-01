using BL.DTOs;
using DAL.UserModel;
using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;

namespace Ui.Services
{
    public class UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : BL.Contracts.IUserService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

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
                Token = "DummyToken"
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
                Id = Guid.Parse( user.Id),
                Email = user.Email!
            };
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .Select(u => new UserReadDto
                {
                    Id = Guid.Parse(u.Id),
                    Email = u.Email!
                })
                .ToListAsync();
        }
    }
}
