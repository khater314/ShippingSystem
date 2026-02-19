using BL.DTOs;
using Domains;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;

namespace BL.Services
{
    public class PaymentMethodService : BaseService<TbPaymentMethod, TbPaymentMethodDTO>, IPaymentMethodService
    {
        public PaymentMethodService(ITableRepository<TbPaymentMethod> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}