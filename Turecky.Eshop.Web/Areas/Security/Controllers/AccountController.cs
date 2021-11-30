using Microsoft.AspNetCore.Mvc;
using Turecky.Eshop.Web.Controllers;
using Turecky.Eshop.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.ApplicationServices.Abstraction;

namespace Turecky.Eshop.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        ISecurityApplicationService security;

        public AccountController(ISecurityApplicationService security)
        {
            this.security = security;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            security.Logout();
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                loginVM.LoginFailed = !await security.Login(loginVM);

                if(loginVM.LoginFailed == false)
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller",String.Empty),new { area = ""});
            }
            return View(loginVM);
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                string[] errors = await security.Register(registerVM, Models.Identity.Roles.Customer);

                if (errors == null)
                {
                    LoginViewModel loginVM = new LoginViewModel()
                    {
                        UserName = registerVM.UserName,
                        Password = registerVM.Password
                    };

                    return await Login(loginVM);
                }

            }
            return View(registerVM);
        }

    }
}
