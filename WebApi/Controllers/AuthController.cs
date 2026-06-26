using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController
        (
        IUserService userService, 
        IRefreshTokenService refreshTokenService,
        IRefreshTokenRetrevalService refreshTokenRetreval,
        TokenService tokenService
        ) 
        : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IRefreshTokenService _refreshToken = refreshTokenService;
        private readonly IRefreshTokenRetrevalService _refreshTokenRetreval = refreshTokenRetreval;
        private readonly TokenService _tokenService = tokenService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.RegisterAsync(request);
            if (!user.IsSuccess) return BadRequest(user.Errors);

            return Ok(user);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userResult = await _userService.LoginAsync(request);
            if (!userResult.IsSuccess) return BadRequest(userResult.Errors);

            var user = await _userService.GetUserByEmailAsync(request.Email); 
            if (user == null) return Unauthorized("Error 404 - User not found!");

            List<Claim> claims = GetUserClaims(user);

            //string refreshToken = _tokenService.GenerateRefreshToken();
            //string accessToken = _tokenService.GenerateAccessToken(claims);

            if (string.IsNullOrEmpty(userResult.RefreshToken))
                userResult.RefreshToken = _tokenService.GenerateRefreshToken();

            TbRefreshTokenDto storedRefreshToken = new()
            {
                UserId = user.Id,
                Token = userResult.RefreshToken,
                Expires = DateTime.UtcNow.AddDays(7) 
            };

            await _refreshToken.RefreshToken(storedRefreshToken);

            
            SetRefreshTokenInCookie(storedRefreshToken);

            return Ok(userResult);
        }


        // Refresh Access & Refresh Token.
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("RefreshToken", out string? refreshToken))
                return Unauthorized("Refresh token is required!");

            var storedToken = await _refreshTokenRetreval.GetByToken(refreshToken);

            if (storedToken == null || string.IsNullOrEmpty(storedToken.Token)) 
                return Unauthorized("Invalid refresh token!");

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            TbRefreshTokenDto newRefreshTokenDto = new()
            {
                UserId = storedToken.UserId,
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            await _refreshToken.RefreshToken(storedToken); // Mark old token as expired

            SetRefreshTokenInCookie(newRefreshTokenDto);

            return Ok(new { RefreshToken = newRefreshToken });
        }

        [HttpPost("refresh-access-token")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                
                return Unauthorized("Refresh token is required!");
            }

            var storedToken = await _refreshTokenRetreval.GetByToken(refreshToken);

            if (storedToken == null)
                return Unauthorized("Token not found in database!");

            if (storedToken.Expires < DateTime.UtcNow)
                return Unauthorized("Token expired!");

            var user = await _userService.GetUserByIdAsync(storedToken.UserId.ToString());
            if (user == null) return Unauthorized("User no longer exists!");

            var claims = GetUserClaims(user);

            var newAccessToken = _tokenService.GenerateAccessToken(claims);

            return Ok(new { AccessToken = newAccessToken });
        }

        private void SetRefreshTokenInCookie(TbRefreshTokenDto refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires.ToUniversalTime(),
                Secure = true, // API must be served over HTTPS for this to work
                SameSite = SameSiteMode.None,
                Path = "/"
            };
            Response.Cookies.Append("RefreshToken", refreshToken.Token, cookieOptions);
        }

        private static List<Claim> GetUserClaims(UserReadDto user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Email),
                new(ClaimTypes.Role, "User") 
            };
            return claims;
        }

        private void comment()
        {
            //[HttpPost("revoke-token")]
            //[Authorize] // لازم يكون عامل Login عشان يلغي التوكن بتاعه
            //public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDto request)
            //{
            //    // لو مبعتش توكن في الـ Body، اسحب اللي في الكوكيز
            //    var token = request.Token ?? Request.Cookies["refreshToken"];

            //    if (string.IsNullOrEmpty(token))
            //        return BadRequest("Token is required!");

            //    var result = await _authService.RevokeTokenAsync(token);

            //    if (!result) return BadRequest("Token is invalid!");

            //    return Ok();
            //}
        }
    }
}
