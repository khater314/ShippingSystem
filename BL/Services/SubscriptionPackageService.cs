using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class SubscriptionPackageService(ITableRepository<TbSubscriptionPackage> repo, IMapper mapper, IUserService userService) : BaseService<TbSubscriptionPackage, TbSubscriptionPackageDTO>(repo, mapper, userService), ISubscriptionPackageService
    {
    }
}