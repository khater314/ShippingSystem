using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class UserContactService(ITableRepository<TbUserContact> repo, IMapper mapper, IUserService userService) : BaseService<TbUserContact, TbUserContactDTO>(repo, mapper, userService), IUserContactService
    {
    }
}