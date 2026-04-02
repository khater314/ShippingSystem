using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class SettingService(ITableRepository<TbSetting> repo, IMapper mapper, IUserService userService) : BaseService<TbSetting, TbSettingDTO>(repo, mapper, userService), ISettingService
    {
    }
}