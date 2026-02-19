using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class UserSubscriptionService : BaseService<TbUserSubscription, TbUserSubscriptionDTO>, IUserSubscriptionService
    {
        public UserSubscriptionService(ITableRepository<TbUserSubscription> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}