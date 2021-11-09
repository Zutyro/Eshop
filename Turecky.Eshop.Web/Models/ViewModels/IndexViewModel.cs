using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.Entity;

namespace Turecky.Eshop.Web.Models.ViewModels
{
    public class IndexViewModel
    {
        public IList<CarouselItem> CarouselItems { get; set; }
        public IList<EshopItem> EshopItems { get; set; }
    }
}
