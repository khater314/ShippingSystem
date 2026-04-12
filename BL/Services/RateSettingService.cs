using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class RateSettingService(ITableRepository<TbRateSetting> repo, IMapper mapper, IUserService userService) : BaseService<TbRateSetting, TbRateSettingDTO>(repo, mapper, userService), IRateSettingService
    {
    }
}