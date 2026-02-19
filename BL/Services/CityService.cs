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
    public class CityService : BaseService<TbCity, TbCityDTO>, ICityService
    {
        public CityService(ITableRepository<TbCity> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
