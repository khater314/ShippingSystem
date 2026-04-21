using BL.Contracts;
using BL.DTOs;
using BL.Services;
using Domains;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    public class UserSubscriptionController
        (IUserSubscriptionService subscriptionService, ISubscriptionPackageService package, IUserService user) : BaseController<TbUserSubscription, TbUserSubscriptionDTO>(subscriptionService)
    {
        private readonly IUserSubscriptionService _subscriptionService = subscriptionService;
        private readonly ISubscriptionPackageService _package = package;
        private readonly IUserService _user = user;

        public override async Task<ActionResult> Index(CancellationToken ct)
        {
            var list = await _subscriptionService.GetAllViewDataAsync(ct);
            return View(list);
        }

        public override async Task<ActionResult> Edit(Guid? id, CancellationToken ct)
        {
            ViewBag.Packages = await _package.GetAllAsync(ct);
            return await base.Edit(id, ct);
        }

        public override async Task<ActionResult> Edit(TbUserSubscriptionDTO dTO, CancellationToken ct)
        {
            dTO.UserId = await _user.GetLoggedInUserId();
            return await base.Edit(dTO, ct);
        }
    }
}
