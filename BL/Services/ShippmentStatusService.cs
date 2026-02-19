using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class ShippmentStatusService : BaseService<TbShippmentStatus, TbShippmentStatusDTO>, IShippmentStatusService
    {
        public ShippmentStatusService(ITableRepository<TbShippmentStatus> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}