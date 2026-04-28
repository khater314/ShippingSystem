using AppResources.Localization.Resources;
using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ui.Services;

namespace Ui.Controllers
{
    [AllowAnonymous]
    public class AccountController(IUserService userService, GenericApiClient httpClient) : Controller
    {
        private readonly IUserService _userService = userService;
        private readonly GenericApiClient _httpClient = httpClient;

        [HttpPost] 
        public async Task<IActionResult> Login(UserLoginDto user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var result = await _userService.LoginAsync(user);

            var tokens = await _httpClient.PostAsync<UserLoginDto, UserResultDto>("api/auth/login", user);

            if (tokens == null || !tokens.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, ResShared.Val_InvalidCredentials);
                return View(user);
            }
            if (tokens.AccessToken == null || tokens.RefreshToken == null)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong.");
                return View(user);
            }

            result.AccessToken = tokens.AccessToken;
            SetAccessTokenInCookie(tokens.AccessToken);

            return RedirectToLocal(user.ReturnUrl);
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

            return RedirectToLocal(user.ReturnUrl);
        }

        public async Task<IActionResult> Logout()
        {
             await _userService.LogoutAsync();
             return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        private void SetAccessTokenInCookie(string accessToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTime.UtcNow.AddMinutes(15),
                Secure = true, // API must be served over HTTPS for this to work
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("accessToken", accessToken, cookieOptions);
        }
    }
}
