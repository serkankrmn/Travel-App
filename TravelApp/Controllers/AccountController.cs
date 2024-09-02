using Microsoft.AspNetCore.Mvc;
using TravelApp.CommonModels;
using TravelApp.Data;
using TravelApp.Models;
using TravelApp.Data.DomainClasses;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace TravelApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        // Dependency Injection
        public AccountController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // Kayıt (Register) Sayfası
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Kayıt İşlemi
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _applicationDbContext.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username || u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Bu kullanıcı adı veya email zaten kayıtlı.");
                    return View(model);
                }

                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
                };

                _applicationDbContext.Users.Add(user);
                await _applicationDbContext.SaveChangesAsync();

                // Kayıttan sonra ana sayfasına yönlendirme
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // Giriş (Login) Sayfası
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //GİRİŞ eski
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _applicationDbContext.Users
        //            .SingleOrDefaultAsync(u => u.Username == model.Username);

        //        if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        //        {
        //            // Giriş başarılı
        //            // Kullanıcıyı oturum açmış olarak işaretleyin
        //            HttpContext.Session.SetString("Username", user.Username);

        //            // Anasayfaya yönlendirin
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
        //        }
        //    }

        //    return View(model);
        //}

        //GİRİŞ yeni
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _applicationDbContext.Users
                    .SingleOrDefaultAsync(u => u.Username == model.Username);

                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre girdiniz lütfen tekrar deneyiniz.");
                }
            }

            return View(model);
        }


        // Oturum Kapatma
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Login", "Account");
        //}

        // Oturum Kapatma Yeni
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }




        // Anasayfa (Index)
        public IActionResult Index()
        {
            return View();
        }
    }
}
