using System;
using System.Collections.Generic;
using System.Text;
using BL.DTOs;

namespace BL.Contracts
{
    public interface IUserService
    {
        Task<UserResultDto> RegisterAsync(UserDto registerDto);
        Task<UserResultDto> LoginAsync(UserDto loginDto);
        Task LogoutAsync();
        Task<UserReadDto?> GetUserByIdAsync(string userId);
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
    }
}
