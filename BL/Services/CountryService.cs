using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class CountryService(ITableRepository<TbCountry> repo, IMapper mapper, IUserService userService) : BaseService<TbCountry, TbCountryDTO>(repo, mapper, userService), ICountryService
    {
    }
}