using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GecisKontrol.Domain.Model.IdentityModel;
using GecisKontrol.Domain.Model.JWT;
using GecisKontrol.Models.LoginModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GecisKontrol.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly JwtSettings _jwtSettings;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }


        public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/"); // Eğer returnUrl null ise anasayfaya yönlendir.

			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
				if (result.Succeeded)
				{
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        // Diğer claimler...
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return LocalRedirect(returnUrl); // Başarılı giriş sonrası returnUrl'e yönlendir.
				}
				else
				{
					ViewBag.LoginError = "Geçersiz giriş denemesi.";
					return View(model);
				}
			}

			// ModelState geçerli değilse formu tekrar göster.
			return View(model);
		}

        [HttpPost("ApiLogin")]
        public async Task<IActionResult> ApiLogin([FromBody] LoginViewModel loginModel)
        {
            // Kullanıcıyı doğrula
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                // JWT oluştur
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            // Token için claim'ler oluştur
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // Diğer claim'ler...
            };

            // Token'ı imzalamak için anahtar ve algoritma
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token ayarlarını oluştur
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes),
                signingCredentials: creds
            );

            // Token'ı döndür
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
