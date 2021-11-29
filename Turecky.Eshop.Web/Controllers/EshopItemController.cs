using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turecky.Eshop.Web.Controllers
{
    public class EshopItemController : Controller
    {
        public IActionResult Detail()
        {
            return View();
        }
    }
}
