using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Contracts
{
    public interface IRefreshTokenRetrevalService
    {
        Task<TbRefreshTokenDto> GetByToken(string? token);
    }
}
