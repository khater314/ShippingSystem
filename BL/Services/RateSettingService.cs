using BL.DTOs;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;
using Domains.Entities;

namespace BL.Services
{
    public class RateSettingService(ITableRepository<TbRateSetting> repo, IMapper mapper, IUserService userService) : BaseService<TbRateSetting, TbRateSettingDTO>(repo, mapper, userService), IRateSettingService
    {
    }
}