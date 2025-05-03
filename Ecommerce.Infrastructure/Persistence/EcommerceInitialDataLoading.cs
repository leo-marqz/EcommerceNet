
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Persistence
{
    public static class EcommerceInitialDataLoading
    {
        public static async Task LoadAsync(
            EcommerceApplicationDbContext _context,UserManager<User> _userManager,
            RoleManager<IdentityRole> _roleManager,ILoggerFactory _loggerFactory
            )
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole(Role.Administrator));
                    await _roleManager.CreateAsync(new IdentityRole(Role.Guess));
                    await _roleManager.CreateAsync(new IdentityRole(Role.User));
                }

                if (!_userManager.Users.Any())
                {
                    var administrator = new User
                    {
                        Name = "Elmer",
                        LastName = "Marquez",
                        UserName = "leomarqz",
                        Email = "leomarqz2020@gmail.com",
                        PhoneNumber = "67479859",
                        AvatarUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.citypng.com%2Fphoto%2F21035%2Fhd-man-user-illustration-icon-transparent-png&psig=AOvVaw1GVTvq3DJ_V61IpthqkE3H&ust=1746395829575000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCLDewc6liI0DFQAAAAAdAAAAABAE",
                    };
                    await _userManager.CreateAsync(administrator, "$ABClm123456");
                    await _userManager.AddToRoleAsync(administrator, Role.Administrator);

                    var usr = new User
                    {
                        Name = "Aly",
                        LastName = "V.",
                        UserName = "aly",
                        Email = "usr@gmail.com",
                        PhoneNumber = "67579859",
                        AvatarUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.citypng.com%2Fphoto%2F21035%2Fhd-man-user-illustration-icon-transparent-png&psig=AOvVaw1GVTvq3DJ_V61IpthqkE3H&ust=1746395829575000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCLDewc6liI0DFQAAAAAdAAAAABAE",
                    };
                    await _userManager.CreateAsync(usr, "$ABCusr123456");
                    await _userManager.AddToRoleAsync(usr, Role.User);
                }

                if(!_context.Categories.Any())
                {
                    string ct_content = File.ReadAllText("../Data/category.json");
                    List<Category>? categories = JsonConvert.DeserializeObject<List<Category>>(ct_content);
                    await _context.Categories.AddRangeAsync(categories!);
                    await _context.SaveChangesAsync();
                }

                if (_context.Products!.Any())
                {
                    string pd_content = File.ReadAllText("../Data/product.json");
                    List<Product>? products = JsonConvert.DeserializeObject<List<Product>>(pd_content);
                    await _context.Products.AddRangeAsync(products!);
                    await _context.SaveChangesAsync();
                }

                if (_context.Images!.Any())
                {
                    string img_content = File.ReadAllText("../Data/image.json");
                    List<Image>? images = JsonConvert.DeserializeObject<List<Image>>(img_content);
                    await _context.Images.AddRangeAsync(images!);
                    await _context.SaveChangesAsync();
                }

                if (_context.Reviews!.Any())
                {
                    string rv_content = File.ReadAllText("../Data/review.json");
                    List<Review>? reviews = JsonConvert.DeserializeObject<List<Review>>(rv_content);
                    await _context.Reviews.AddRangeAsync(reviews!);
                    await _context.SaveChangesAsync();
                }

                if (_context.Countries!.Any())
                {
                    string cou_content = File.ReadAllText("../Data/countries.json");
                    List<Country>? countries = JsonConvert.DeserializeObject<List<Country>>(cou_content);
                    await _context.Countries.AddRangeAsync(countries!);
                    await _context.SaveChangesAsync();
                }

            }
            catch(Exception ex)
            {
                var logger = _loggerFactory.CreateLogger(nameof(EcommerceInitialDataLoading));
                logger.LogError(ex.Message);
            }
        }
    }
}
