using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class SettingService : BaseService<TbSetting, TbSettingDTO>, ISettingService
    {
        public SettingService(ITableRepository<TbSetting> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}