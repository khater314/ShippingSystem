using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class ShippmentService(ITableRepository<TbShippment> repo, IMapper mapper, IUserService userService) : BaseService<TbShippment, TbShippmentDTO>(repo, mapper, userService), IShippmentService
    {
    }
}