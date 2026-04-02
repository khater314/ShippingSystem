//using AutoMapper;
using BL.Mapping;
using BL.Contracts;
using DAL.Contracts;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class BaseService<T, DTO>(ITableRepository<T> repo, IMapper mapper, IUserService userService) : IBaseService<T, DTO> where T : BaseEntity
    {
        private readonly ITableRepository<T> _repo = repo;
        private readonly BL.Mapping.IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;

        /* Request Data */
        public async Task<IEnumerable<DTO>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _repo.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<T>, IEnumerable<DTO>>(list);
        }
        public async Task<DTO> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            return _mapper.Map<T, DTO>(entity);
        }

        /* Response Data */
        public async Task AddAsync(DTO entity, CancellationToken ct = default)
        {
            var dbEntity = _mapper.Map<DTO, T>(entity);
            dbEntity.CurrentState = 1;
            dbEntity.CreatedBy = _userService.GetLoggedInUserId();
            await _repo.AddAsync(dbEntity, ct);
        }
        public async Task UpdateAsync(DTO entity, CancellationToken ct = default)
        {
            var dbEntity = _mapper.Map<DTO, T>(entity);
            dbEntity.UpdatedBy = _userService.GetLoggedInUserId();
            await _repo.UpdateAsync(dbEntity, ct);
        }
        public async Task ChangeStatusAsync(DTO entity, int status = 1, CancellationToken ct = default)
        {
            var dbEntity = _mapper.Map<DTO, T>(entity);
            dbEntity.UpdatedBy = _userService.GetLoggedInUserId();
            await _repo.ChangeStatusAsync(dbEntity.Id, status, ct);
        }




    }
}
