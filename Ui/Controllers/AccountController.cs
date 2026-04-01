using AppResources.Localization.Resources;
using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    [AllowAnonymous]
    public class AccountController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;
        [HttpPost] 
        public async Task<IActionResult> Login(UserLoginDto user)
        {
            if (!ModelState.IsValid)
                return View(user);

           var result = await _userService.LoginAsync(user);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, ResShared.Val_InvalidCredentials);
                return View(user);
            }
            if (!string.IsNullOrEmpty(user.ReturnUrl) && Url.IsLocalUrl(user.ReturnUrl))
                return Redirect(user.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new UserLoginDto() { Email = "", Password = "", ReturnUrl = returnUrl });
        }
        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            UserRegisterDto user = new() 
            { 
                Email = "", 
                Password = "", 
                ConfirmedPassword = "", 
                ReturnUrl = returnUrl
            };
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var result = await _userService.RegisterAsync(user);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, "Something Wrong!");
                return View(user);
            }

            if (!string.IsNullOrEmpty(user.ReturnUrl) && Url.IsLocalUrl(user.ReturnUrl))
                return Redirect(user.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
