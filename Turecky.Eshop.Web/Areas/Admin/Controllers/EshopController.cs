using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.Database;
using Turecky.Eshop.Web.Models.Entity;
using Turecky.Eshop.Web.Models.Identity;
using Turecky.Eshop.Web.Models.ViewModels;

namespace Turecky.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class EshopController : Controller
    {

        readonly EshopDbContext eshopDbContext;

        public EshopController(EshopDbContext eshopDB)
        {
            eshopDbContext = eshopDB;
        }

        public IActionResult Select()
        {
            IndexViewModel indexVM = new IndexViewModel();
            indexVM.EshopItems = eshopDbContext.EshopItems.ToList();

            return View(indexVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EshopItem eshopItem)
        {
            if (String.IsNullOrWhiteSpace(eshopItem.Name) == false
                && String.IsNullOrWhiteSpace(eshopItem.Description) == false)
            {
                //Pouze pro DatabaseFake
                /*if (DatabaseFake.EshopItems != null && DatabaseFake.EshopItems.Count > 0)
                {
                    eshopItem.ID = DatabaseFake.EshopItems.Last().ID + 1;
                }*/

                eshopDbContext.EshopItems.Add(eshopItem);

                await eshopDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(EshopController.Select));
            }
            return View(eshopItem);
        }

        public IActionResult Edit(int ID)
        {
            EshopItem eshopItem = eshopDbContext.EshopItems.FirstOrDefault(ei => ei.ID == ID);

            if (eshopItem != null)
            {
                return View(eshopItem);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EshopItem eItem)
        {
            EshopItem eshopItem = eshopDbContext.EshopItems.FirstOrDefault(ei => ei.ID == eItem.ID);

            if (eshopItem != null)
            {
                if (String.IsNullOrWhiteSpace(eItem.Name) == false
                && String.IsNullOrWhiteSpace(eItem.Description) == false)
                {
                    eshopItem.Name = eItem.Name;
                    eshopItem.Description = eItem.Description;

                    await eshopDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(EshopController.Select));
                }

                return View(eshopItem);
            }

            return NotFound();
        }
        public async Task<IActionResult> Delete(int ID)
        {
            EshopItem eshopItem = eshopDbContext.EshopItems.Where(eItem => eItem.ID == ID).FirstOrDefault();

            if (eshopItem != null)
            {
                eshopDbContext.EshopItems.Remove(eshopItem);

                await eshopDbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(EshopController.Select));
        }
    }
}
