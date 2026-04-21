using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class UserSubscriptionService
        (ITableRepository<TbUserSubscription> repo, 
        IMapper mapper, 
        IUserService userService, 
        IViewRepository<VwUserSubscription> viewRepo) 
        : BaseService<TbUserSubscription, TbUserSubscriptionDTO>(repo, mapper, userService), IUserSubscriptionService
    {

        private readonly IViewRepository<VwUserSubscription> _viewRepo = viewRepo;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;

        public async Task<IEnumerable<TbUserSubscriptionDTO>> GetAllViewDataAsync(CancellationToken ct = default)
        {
            var list = await _viewRepo.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<VwUserSubscription>, List<TbUserSubscriptionDTO>>(list);
        }
    }
}