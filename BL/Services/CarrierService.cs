using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class CarrierService : BaseService<TbCarrier, TbCarrierDTO>, ICarrierService
    {
        public CarrierService(ITableRepository<TbCarrier> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}