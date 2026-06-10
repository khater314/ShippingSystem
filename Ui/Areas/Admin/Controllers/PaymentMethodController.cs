using BL.Contracts;
using BL.DTOs;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    public class PaymentMethodController(IPaymentMethodService paymentMethodService) : BaseController<TbPaymentMethod, TbPaymentMethodDTO>(paymentMethodService)
    {
        private readonly IPaymentMethodService _paymentMethodService = paymentMethodService;
    }
}
