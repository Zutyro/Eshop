using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models;
using Turecky.Eshop.Web.Models.Database;
using Turecky.Eshop.Web.Models.ViewModels;

namespace Turecky.Eshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        readonly EshopDbContext eshopDbContext;

        public HomeController(ILogger<HomeController> logger, EshopDbContext eshopDB)
        {
            _logger = logger;
            eshopDbContext = eshopDB;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Byla zobrazena hlavni stranka");
            IndexViewModel indexVM = new IndexViewModel();
            indexVM.CarouselItems = eshopDbContext.CarouselItems.ToList();
            indexVM.EshopItems = eshopDbContext.EshopItems.ToList();

            return View(indexVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
