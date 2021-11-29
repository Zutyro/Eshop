using Microsoft.AspNetCore.Mvc;
using Turecky.Eshop.Web.Controllers;
using Turecky.Eshop.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turecky.Eshop.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""), new { area = "" });
            }
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller",""),new { area = ""});
            }
            return View();
        }

    }
}
