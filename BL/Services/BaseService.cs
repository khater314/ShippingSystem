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
    public class BaseService<T, DTO> : IBaseService<T, DTO> where T : BaseEntity
    {
        private readonly ITableRepository<T> _repo;
        private readonly BL.Mapping.IMapper _mapper;
        public BaseService(ITableRepository<T> repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }


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
        public async Task AddAsync(DTO entity, Guid userId, CancellationToken ct = default)
        {
            var dbEntity = _mapper.Map<DTO, T>(entity);
            dbEntity.CurrentState = 1;
            dbEntity.CreatedBy = userId;
            await _repo.AddAsync(dbEntity, ct);
        }
        public async Task UpdateAsync(DTO entity, Guid userId, CancellationToken ct = default)
        {
            var dbEntity = _mapper.Map<DTO, T>(entity);
            dbEntity.UpdatedBy = userId;
            await _repo.UpdateAsync(dbEntity, ct);
        }
        public async Task ChangeStatusAsync(Guid id, Guid userId, int status = 1, CancellationToken ct = default)
        {
            await _repo.ChangeStatusAsync(id, status, ct);
        }




    }
}
