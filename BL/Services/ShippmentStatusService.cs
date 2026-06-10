using BL.DTOs;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;
using Domains.Entities;

namespace BL.Services
{
    public class ShipmentStatusService(ITableRepository<TbShipmentStatus> repo, IMapper mapper, IUserService userService) : BaseService<TbShipmentStatus, TbShipmentStatusDTO>(repo, mapper, userService), IShipmentStatusService
    {
    }
}