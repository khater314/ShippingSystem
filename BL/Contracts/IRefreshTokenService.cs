using BL.DTOs;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Contracts
{
    public interface IRefreshTokenService : IBaseService<TbRefreshToken, TbRefreshTokenDto>
    {
        Task<bool> RefreshToken(TbRefreshTokenDto tokenDto);
    }
}
