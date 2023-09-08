using IEL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IEL.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (model.Email == "caio@1234" && model.Password == "1234")
                {
                    return RedirectToAction("Index", "Estudante");
                }

                // If we reach this point, login failed.
                TempData["Error"] = "Invalid login attempt.";
            }

            return View(model);
        }

    }
}
