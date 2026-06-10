using BL.DTOs;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;
using Domains.Entities;

namespace BL.Services
{
    public class SubscriptionPackageService(ITableRepository<TbSubscriptionPackage> repo, IMapper mapper, IUserService userService) : BaseService<TbSubscriptionPackage, TbSubscriptionPackageDTO>(repo, mapper, userService), ISubscriptionPackageService
    {
    }
}