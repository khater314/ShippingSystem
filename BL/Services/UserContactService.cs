using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;


namespace BL.Services
{
    public class UserContactService
        (
        ITableRepository<TbUserContact> repo,
        IMapper mapper,
        IUserService userService,
        IViewRepository<VwUserContact> viewRepo,
        IUnitOfWork unitOfWork
        )
        : BaseService<TbUserContact, TbUserContactDTO>(unitOfWork, mapper, userService), IUserContactService
    {
        private readonly IViewRepository<VwUserContact> _viewRepo = viewRepo;
        private readonly ITableRepository<TbUserContact> _repo = repo;
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TbUserContactDTO>> GetAllViewDataAsync(CancellationToken ct = default)
        {
            var list = await _viewRepo.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<VwUserContact>, IEnumerable<TbUserContactDTO>>(list);
        }

        public override async Task<Guid> AddAndGetIdAsync(TbUserContactDTO entity, CancellationToken ct = default)
        {
            entity.UserId = await _userService.GetLoggedInUserId(); 
            return await base.AddAndGetIdAsync(entity, ct); 
        }
    }
}