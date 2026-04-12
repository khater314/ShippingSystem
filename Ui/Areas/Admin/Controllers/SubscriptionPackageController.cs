using BL.Contracts;
using BL.DTOs;
using Domains;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    public class SubscriptionPackageController(ISubscriptionPackageService subscriptionPackageService) : BaseController<TbSubscriptionPackage, TbSubscriptionPackageDTO>(subscriptionPackageService)
    {
        private readonly ISubscriptionPackageService _subscriptionPackageService = subscriptionPackageService;
    }
}
