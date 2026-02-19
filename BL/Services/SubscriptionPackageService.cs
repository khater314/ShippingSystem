using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class SubscriptionPackageService : BaseService<TbSubscriptionPackage, TbSubscriptionPackageDTO>, ISubscriptionPackageService
    {
        public SubscriptionPackageService(ITableRepository<TbSubscriptionPackage> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}