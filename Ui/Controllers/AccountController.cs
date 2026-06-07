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

            // Break Point Here!
            if (!result.IsSuccess || tokens == null || !tokens.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, ResShared.Val_InvalidCredentials);
                return View(user);
            }
            if (tokens.AccessToken == null || tokens.RefreshToken == null)
            {
                ModelState.AddModelError(string.Empty, "Tokens went wrong.");
                return View(user);
            }

            result.AccessToken = tokens.AccessToken;
            
            SetAccessTokenInCookie(tokens.AccessToken);

            TbRefreshTokenDto refreshToken = new() 
            { 
                Token = tokens.RefreshToken, 
                Expires = DateTime.UtcNow.AddDays(7), 
                UserId = await _userService.GetLoggedInUserId()
            };
            SetRefreshTokenInCookie(refreshToken);

            return Redirect(user.ReturnUrl ?? Url.Action("Index", "Home") ?? "/");
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
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
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

            var login = new UserLoginDto { 
                Email = user.Email, 
                Password = user.Password, 
                ReturnUrl = user.ReturnUrl 
            };

            return await Login(login);
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
                SameSite = SameSiteMode.Lax,
                Path = "/"
            };
            Response.Cookies.Append("AccessToken", accessToken, cookieOptions);
        }
        private void SetRefreshTokenInCookie(TbRefreshTokenDto refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires.ToUniversalTime(),
                Secure = true, // API must be served over HTTPS for this to work
                SameSite = SameSiteMode.Lax,
                Path = "/"
            };
            Response.Cookies.Append("RefreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
