using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var accessToken = await _authService.LoginAsync(userLogin);
            if (accessToken != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login", accessToken);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            var accessToken = await _authService.RegisterAsync(userRegister);
            if (accessToken != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Register", accessToken);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
