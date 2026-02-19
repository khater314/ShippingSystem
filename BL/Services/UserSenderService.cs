using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class UserSenderService : BaseService<TbUserSender, TbUserSenderDTO>, IUserSenderService
    {
        public UserSenderService(ITableRepository<TbUserSender> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}