using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class ShippingTypeService : BaseService<TbShippingType, TbShippingTypeDTO>, IShippingTypeService
    {
        public ShippingTypeService(ITableRepository<TbShippingType> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}