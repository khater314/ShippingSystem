using BL.DTOs;
using BL.Contracts;
using DAL.Contracts;
using BL.Mapping;
using Domains.Entities;

namespace BL.Services
{
    public class PaymentMethodService(ITableRepository<TbPaymentMethod> repo, IMapper mapper, IUserService userService) : BaseService<TbPaymentMethod, TbPaymentMethodDTO>(repo, mapper, userService), IPaymentMethodService
    {
    }
}