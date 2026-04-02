using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class UserSenderService(ITableRepository<TbUserSender> repo, IMapper mapper, IUserService userService) : BaseService<TbUserSender, TbUserSenderDTO>(repo, mapper, userService), IUserSenderService
    {
    }
}