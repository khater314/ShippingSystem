using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class PackagingService(ITableRepository<TbPackaging> repo, IMapper mapper, IUserService userService) : BaseService<TbPackaging, TbPackagingDTO>(repo, mapper, userService), IPackagingService
    {
    }
}