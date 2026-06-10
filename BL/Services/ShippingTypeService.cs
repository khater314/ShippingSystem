using BL.DTOs;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;
using Domains.Entities;

namespace BL.Services
{
    public class ShippingTypeService(ITableRepository<TbShippingType> repo, IMapper mapper, IUserService userService) : BaseService<TbShippingType, TbShippingTypeDTO>(repo, mapper, userService), IShippingTypeService
    {
    }
}