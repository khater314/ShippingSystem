using BL.DTOs;
using Domains;
using BL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class CityService
        (
        IMapper mapper,
        IUserService userService, 
        ITableRepository<TbCity> repo, 
        IViewRepository<VwCity> viewRepo
        ) 
        : BaseService<TbCity, TbCityDTO>(repo, mapper, userService), ICityService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ITableRepository<TbCity> _repo = repo;
        private readonly IUserService _userService = userService;
        private readonly IViewRepository<VwCity> _viewRepo = viewRepo;
        public async Task<IEnumerable<TbCityDTO>> GetAllCountryCitiesAsync(CancellationToken ct = default)
        {
            var cities = await _viewRepo.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<VwCity>, IEnumerable<TbCityDTO>>(cities);
        }
    }
}
