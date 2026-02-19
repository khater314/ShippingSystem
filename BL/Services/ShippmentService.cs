using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class ShippmentService : BaseService<TbShippment, TbShippmentDTO>, IShippmentService
    {
        public ShippmentService(ITableRepository<TbShippment> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}