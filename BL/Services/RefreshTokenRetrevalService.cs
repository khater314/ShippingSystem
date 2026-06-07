using AutoMapper;
using BL.Contracts;
using BL.DTOs;
using DAL.Contracts;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class RefreshTokenRetrevalService
        (
        ITableRepository<TbRefreshToken> repo, 
        IMapper mapper
        ) 
        : IRefreshTokenRetrevalService
    {
        private readonly ITableRepository<TbRefreshToken> _repo = repo;
        private readonly IMapper _mapper = mapper;
        public async Task<TbRefreshTokenDto> GetByToken(string? token)
        {
            var refreshToken = await _repo.GetFirstOrDefaultAsync(x => x.Token == token);

            return refreshToken == null
                ? throw new Exception("Refresh token not found, User Not Authorized")
                : _mapper.Map<TbRefreshToken, TbRefreshTokenDto>(refreshToken);
        }
    }
}
