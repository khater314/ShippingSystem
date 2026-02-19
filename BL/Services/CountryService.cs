using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class CountryService : BaseService<TbCountry, TbCountryDTO>, ICountryService
    {
        public CountryService(ITableRepository<TbCountry> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}