using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class ShippmentStatusService(ITableRepository<TbShippmentStatus> repo, IMapper mapper, IUserService userService) : BaseService<TbShippmentStatus, TbShippmentStatusDTO>(repo, mapper, userService), IShippmentStatusService
    {
    }
}