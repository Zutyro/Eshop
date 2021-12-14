using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.Entity;
using Turecky.Eshop.Web.Models.Identity;

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
                Description = "Opticka USB mys pro PC.",
                Price = 250
            };
            EshopItem ei2 = new EshopItem()
            {
                ID = 1,
                Name = "Pevny disk",
                Description = "Mechanicky pevny disk s 7200 otacek za minutu a ulozistem 1TB.",
                Price = 1150
            };
            EshopItem ei3 = new EshopItem()
            {
                ID = 2,
                Name = "Klavesnice",
                Description = "USB klavesnice pro PC se standardnim rozlozenim.",
                Price = 500
            };

            eshopItems.Add(ei1);
            eshopItems.Add(ei2);
            eshopItems.Add(ei3);


            return eshopItems;
        }

        public async Task EnsureRoleCreated(RoleManager<Role> roleManager)
        {
            string[] roles = Enum.GetNames(typeof(Roles));

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new Role(role));
            }
        }

        public async Task EnsureAdminCreated(UserManager<User> userManager)
        {
            User user = new User
            {
                UserName = "admin",
                Email = "admin@admin.cz",
                EmailConfirmed = true,
                FirstName = "Tomas",
                LastName = "Turecky"
            };
            string password = "abc";

            User adminInDatabase = await userManager.FindByNameAsync(user.UserName);

            if (adminInDatabase == null)
            {

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result == IdentityResult.Success)
                {
                    string[] roles = Enum.GetNames(typeof(Roles));
                    foreach (var role in roles)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
                else if (result != null && result.Errors != null && result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        Debug.WriteLine($"Error during Role creation for Admin: {error.Code}, {error.Description}");
                    }
                }
            }

        }

        public async Task EnsureManagerCreated(UserManager<User> userManager)
        {
            User user = new User
            {
                UserName = "manager",
                Email = "manager@manager.cz",
                EmailConfirmed = true,
                FirstName = "Tomas",
                LastName = "Turecky"
            };
            string password = "abc";

            User managerInDatabase = await userManager.FindByNameAsync(user.UserName);

            if (managerInDatabase == null)
            {

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result == IdentityResult.Success)
                {
                    string[] roles = Enum.GetNames(typeof(Roles));
                    foreach (var role in roles)
                    {
                        if (role != Roles.Admin.ToString())
                            await userManager.AddToRoleAsync(user, role);
                    }
                }
                else if (result != null && result.Errors != null && result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        Debug.WriteLine($"Error during Role creation for Manager: {error.Code}, {error.Description}");
                    }
                }
            }

        }
    }
}
