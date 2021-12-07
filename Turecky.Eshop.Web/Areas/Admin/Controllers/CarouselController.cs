using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.Database;
using Turecky.Eshop.Web.Models.Entity;
using Turecky.Eshop.Web.Models.Identity;
using Turecky.Eshop.Web.Models.Implementation;
using Turecky.Eshop.Web.Models.ViewModels;

namespace Turecky.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class CarouselController : Controller
    {
        readonly EshopDbContext eshopDbContext;
        IWebHostEnvironment env;

        public CarouselController(EshopDbContext eshopDB, IWebHostEnvironment env)
        {
            this.env = env;
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
            if (carouselItem != null && carouselItem.Image != null)
            {

                FileUpload fileUpload = new FileUpload(env.WebRootPath, "img/CarouselItems", "image");
                carouselItem.ImageSrc = await fileUpload.FileUploadAsync(carouselItem.Image);

                ModelState.Clear();
                TryValidateModel(carouselItem);
                if (ModelState.IsValid)
                {
                    eshopDbContext.CarouselItems.Add(carouselItem);

                    await eshopDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(CarouselController.Select));


                }
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
