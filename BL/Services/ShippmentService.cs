using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class ShipmentService(ITableRepository<TbShipment> repo, IMapper mapper, IUserService userService) : BaseService<TbShipment, TbShipmentDTO>(repo, mapper, userService), IShipmentService
    {
    }
}