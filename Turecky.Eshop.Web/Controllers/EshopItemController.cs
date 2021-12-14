using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.Database;
using Turecky.Eshop.Web.Models.Entity;

namespace Turecky.Eshop.Web.Controllers
{
    public class EshopItemController : Controller
    {
        readonly EshopDbContext eshopDbContext;

        public EshopItemController(EshopDbContext eshopDB)
        {
            eshopDbContext = eshopDB;
        }

        public IActionResult Detail(int ID)
        {
            EshopItem eshopItem = eshopDbContext.EshopItems.FirstOrDefault(ei => ei.ID == ID);

            if (eshopItem != null)
            {
                return View(eshopItem);
            }

            return NotFound();
        }
    }
}
