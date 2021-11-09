using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.Database;
using Turecky.Eshop.Web.Models.Entity;
using Turecky.Eshop.Web.Models.ViewModels;

namespace Turecky.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselController : Controller
    {
        readonly EshopDbContext eshopDbContext;

        public CarouselController(EshopDbContext eshopDB)
        {
            eshopDbContext = eshopDB;
        }

        public IActionResult Select()
        {
            IndexViewModel indexVM = new IndexViewModel();
            indexVM.CarouselItems = eshopDbContext.CarouselItems.ToList();

            return View(indexVM);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarouselItem carouselItem)
        {
            if(String.IsNullOrWhiteSpace(carouselItem.ImageSrc) == false
                && String.IsNullOrWhiteSpace(carouselItem.ImageAlt) == false)
            {
                //Pouze pro DatabaseFake
                /*if(eshopDbContext.CarouselItems != null && eshopDbContext.CarouselItems.Count() > 0)
                {
                    carouselItem.ID = eshopDbContext.CarouselItems.Last().ID + 1;
                }*/

                eshopDbContext.CarouselItems.Add(carouselItem);

                await eshopDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(CarouselController.Select));
            }
            return View(carouselItem);
        }

        public IActionResult Edit(int ID)
        {
            CarouselItem carouselItem = eshopDbContext.CarouselItems.FirstOrDefault(ci => ci.ID == ID);

            if(carouselItem != null)
            {
                return View(carouselItem);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarouselItem cItem)
        {
            CarouselItem carouselItem = eshopDbContext.CarouselItems.FirstOrDefault(ci => ci.ID == cItem.ID);

            if (carouselItem != null)
            {
                if (String.IsNullOrWhiteSpace(cItem.ImageSrc) == false
                && String.IsNullOrWhiteSpace(cItem.ImageAlt) == false)
                {
                    carouselItem.ImageAlt = cItem.ImageAlt;
                    carouselItem.ImageSrc = cItem.ImageSrc;

                    await eshopDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(CarouselController.Select));
                }

                return View(carouselItem);
            }

            return NotFound();
        }
        public async Task<IActionResult> Delete(int ID)
        {
            CarouselItem carouselItem = eshopDbContext.CarouselItems.Where(cItem => cItem.ID == ID).FirstOrDefault();

            if(carouselItem != null)
            {
                eshopDbContext.CarouselItems.Remove(carouselItem);

                await eshopDbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(CarouselController.Select));
        }

    }
}
