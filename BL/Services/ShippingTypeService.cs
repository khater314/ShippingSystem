using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class ShippingTypeService(ITableRepository<TbShippingType> repo, IMapper mapper, IUserService userService) : BaseService<TbShippingType, TbShippingTypeDTO>(repo, mapper, userService), IShippingTypeService
    {
    }
}