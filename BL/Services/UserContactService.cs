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
        IViewRepository<VwUserContact> viewRepo
        )
        : BaseService<TbUserContact, TbUserContactDTO>(repo, mapper, userService), IUserContactService
    {
        private readonly IViewRepository<VwUserContact> _viewRepo = viewRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<TbUserContactDTO>> GetAllViewDataAsync(CancellationToken ct = default)
        {
            var list = await _viewRepo.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<VwUserContact>, IEnumerable<TbUserContactDTO>>(list);
        }
    }
}