using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.Entity;

namespace Turecky.Eshop.Web.Models.Database
{
    public class DatabaseInit
    {

        public void Initialization(EshopDbContext eshopDbContext)
        {
            eshopDbContext.Database.EnsureCreated();

            if(eshopDbContext.CarouselItems.Count() == 0)
            {
                IList<CarouselItem> carouselItems = GenerateCarouselItems();
                eshopDbContext.CarouselItems.AddRange(carouselItems);
                //eshopDbContext.SaveChanges();
            }

            if (eshopDbContext.EshopItems.Count() == 0)
            {
                IList<EshopItem> eshopItems = GenerateEshopItems();
                eshopDbContext.EshopItems.AddRange(eshopItems);
                //eshopDbContext.SaveChanges();
            }
            //Nemuzu volat SaveChanges() dvakrat v jedne metode
            eshopDbContext.SaveChanges();
        }

        public List<CarouselItem> GenerateCarouselItems()
        {
            List<CarouselItem> carouselItems = new List<CarouselItem>();

            CarouselItem ci1 = new CarouselItem()
            {
                ID = 0,
                ImageSrc = "/img/inf1.jfif",
                ImageAlt = "First slide"
            };
            CarouselItem ci2 = new CarouselItem()
            {
                ID = 1,
                ImageSrc = "/img/inf2.jfif",
                ImageAlt = "Second slide"
            };
            CarouselItem ci3 = new CarouselItem()
            {
                ID = 2,
                ImageSrc = "/img/inf3.jpg",
                ImageAlt = "Third slide"
            };

            carouselItems.Add(ci1);
            carouselItems.Add(ci2);
            carouselItems.Add(ci3);

            return carouselItems;
        }

        public List<EshopItem> GenerateEshopItems()
        {
            List<EshopItem> eshopItems = new List<EshopItem>();

            EshopItem ei1 = new EshopItem()
            {
                ID = 0,
                Name = "Opticka mys",
                Description = "Opticka USB mys pro PC."
            };
            EshopItem ei2 = new EshopItem()
            {
                ID = 1,
                Name = "Pevny disk",
                Description = "Mechanicky pevny disk s 7200 otacek za minutu."
            };
            EshopItem ei3 = new EshopItem()
            {
                ID = 2,
                Name = "Klavesnice",
                Description = "USB klavesnice pro PC se standardnim rozlozenim."
            };

            eshopItems.Add(ei1);
            eshopItems.Add(ei2);
            eshopItems.Add(ei3);


            return eshopItems;
        }
    }
}
