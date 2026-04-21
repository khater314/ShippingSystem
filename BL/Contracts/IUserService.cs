using System;
using System.Collections.Generic;
using System.Text;
using BL.DTOs;

namespace BL.Contracts
{
    public interface IUserService
    {
        Task<UserResultDto> RegisterAsync(UserRegisterDto registerDto);
        Task<UserResultDto> LoginAsync(UserLoginDto loginDto);
        Task LogoutAsync();
        Task<UserReadDto?> GetUserByIdAsync(string userId);
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<Guid> GetLoggedInUserId();
        Task<IEnumerable<UserReadDto>> GetUsersBySelectedIdsAsync(List<string> ids);
    }
}
