using System;
using System.Collections.Generic;
using System.Text;
using BL.DTOs;
using Domains.Entities;

namespace BL.Contracts
{
    public interface ICityService : IBaseService<TbCity, TbCityDTO>
    {
        Task<IEnumerable<TbCityDTO>> GetAllCountryCitiesAsync(CancellationToken ct = default);
        Task<IEnumerable<TbCityDTO>> GetCitiesByCountryIdAsync(Guid countryId, 
            CancellationToken ct = default);
    }
}
