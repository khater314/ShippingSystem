using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class ShipmentStatusService(ITableRepository<TbShipmentStatus> repo, IMapper mapper, IUserService userService) : BaseService<TbShipmentStatus, TbShipmentStatusDTO>(repo, mapper, userService), IShipmentStatusService
    {
    }
}