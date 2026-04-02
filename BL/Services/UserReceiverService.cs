using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class UserReceiverService(ITableRepository<TbUserReceiver> repo, IMapper mapper, IUserService userService) : BaseService<TbUserReceiver, TbUserReceiverDTO>(repo, mapper, userService), IUserReceiverService
    {
    }
}