using BL.DTOs;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;
using Domains.Entities;

namespace BL.Services
{
    public class CountryService(ITableRepository<TbCountry> repo, IMapper mapper, IUserService userService) : BaseService<TbCountry, TbCountryDTO>(repo, mapper, userService), ICountryService
    {
    }
}