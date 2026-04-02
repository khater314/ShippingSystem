using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class UserSubscriptionService(ITableRepository<TbUserSubscription> repo, IMapper mapper, IUserService userService) : BaseService<TbUserSubscription, TbUserSubscriptionDTO>(repo, mapper, userService), IUserSubscriptionService
    {
    }
}