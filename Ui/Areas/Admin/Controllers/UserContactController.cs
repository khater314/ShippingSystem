using BL.Contracts;
using BL.DTOs;
using Domains;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    public class UserContactController
        (IUserContactService contactService, ICityService cityService, IUserService user) 
        : BaseController<TbUserContact, TbUserContactDTO>(contactService)
    {
        private readonly IUserContactService _contactService = contactService;
        private readonly ICityService _cityService = cityService;
        private readonly IUserService _userService = user;

        public override async Task<ActionResult> Index(CancellationToken ct)
        {
            var list = await _contactService.GetAllViewDataAsync(ct);
            return View(list);
        }

        public override async Task<ActionResult> Edit(Guid? id, CancellationToken ct)
        {
            ViewBag.Cities = await _cityService.GetAllAsync(ct);
            return await base.Edit(id, ct);
        }

        public override async Task<ActionResult> Edit(TbUserContactDTO dTO, CancellationToken ct)
        {
            dTO.UserId = await _userService.GetLoggedInUserId();
            return await base.Edit(dTO, ct);
        }
    }
}
