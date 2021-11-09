using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.Entity;

namespace Turecky.Eshop.Web.Models.Database
{
    public static class DatabaseFake
    {
        public static List<CarouselItem> CarouselItems { get; set; }
        public static List<EshopItem> EshopItems { get; set; }

        static DatabaseFake()
        {
            DatabaseInit dbInit = new DatabaseInit();
            CarouselItems = dbInit.GenerateCarouselItems();
            EshopItems = dbInit.GenerateEshopItems();
        }
    }
}
