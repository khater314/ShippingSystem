using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="mapper"></param>
    /// <param name="userService"></param>
    public class RefreshTokenService(
        ITableRepository<TbRefreshToken> repo, 
        IMapper mapper, 
        IUserService userService
        ) 
        : BaseService<TbRefreshToken, TbRefreshTokenDto>(repo, mapper, userService), IRefreshTokenService
    {
        private readonly ITableRepository<TbRefreshToken> _repo = repo;
        private readonly IMapper _mapper = mapper;


        /// <summary>
        /// Refresh RefreshToken by marking the old token as expired and creating a new one. This ensures that only one valid refresh token exists for a user at any time.
        /// </summary>
        /// <param name="tokenDto">New Refresh Token</param>
        /// <returns></returns>
        public async Task<bool> RefreshToken(TbRefreshTokenDto tokenDto)
        {
            var expiredTokens = await _repo.GetListAsync(x => x.UserId == tokenDto.UserId && x.CurrentState == 1);
            foreach (var token in expiredTokens)
            {
                await _repo.ChangeStatusAsync(token.Id, 2); // Mark as expired
            }
            var newToken = _mapper.Map<TbRefreshTokenDto, TbRefreshToken>(tokenDto);
            await _repo.AddAsync(newToken);
            return true;
        }
    }
}