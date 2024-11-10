using ApiStore.Constants;
using ApiStore.Data;
using ApiStore.Data.Entities;
using ApiStore.Data.Entities.Identity;
using ApiStore.Interfaces;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace ApiStore.Services
{
    public class DataSeeder
    {
        private readonly ApiStoreDbContext _context;
        private readonly IImageTool _imageTool;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        public DataSeeder(ApiStoreDbContext context, IImageTool imageTool, UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager)
        {
            _context = context;
            _imageTool = imageTool;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                int number = 10;
                var list = new Faker("uk")
                    .Commerce.Categories(number);

                var categories = new List<CategoryEntity>();
                foreach (var name in list)
                {
                    string image = await _imageTool.SaveImageByUrl("https://picsum.photos/1200/800?category");
                    var cat = new CategoryEntity
                    {
                        Name = name,
                        Description = new Faker("uk").Commerce.ProductDescription(),
                        Image = image
                    };
                    categories.Add(cat);
                }

                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }
        }

        public async Task SeedProductsAsync()
        {
            if (_context.Products.Any()) return;

            Faker faker = new Faker();
            var categories = _context.Categories.Select(c => c.Id).ToList();
            var url = "https://picsum.photos/1200/800?product";

            var fakeProduct = new Faker<ProductEntity>()
                .RuleFor(p => p.Name, f => f.Commerce.Product())
                .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(p => p.CategoryId, f => f.PickRandom(categories));

            var products = fakeProduct.Generate(30);
            var r = new Random();

            _context.Products.AddRange(products);
            _context.SaveChanges();

            foreach (var product in products)
            {
                int count = r.Next(3, 5);
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        var fname = await _imageTool.SaveImageByUrl(url);
                        var imageProduct = new ProductImageEntity
                        {
                            ProductId = product.Id,
                            Image = fname,
                            Priority = i
                        };
                        _context.ProductImages.Add(imageProduct);
                    }
                    catch
                    {
                        continue;
                    }
                    
                }
            }
            _context.SaveChanges();
        }

        public async Task SeedRolesAsync()
        {
            if (!_context.Roles.Any())
            {
                var roles = new[]
                {
                    new RoleEntity { Name = Roles.Admin },
                    new RoleEntity { Name = Roles.User }
                };

                foreach (var role in roles)
                {
                    var outcome = await _roleManager.CreateAsync(role);
                    if (!outcome.Succeeded) Console.WriteLine($"Failed to create role: {role.Name}");
                }
            }
        }

        public async Task SeedUsersAsync()
        {
            if (!_context.Users.Any())
            {
                var users = new[]
                {
                    new { User = new UserEntity { FirstName = "Tony", LastName = "Stark", UserName = "admin@gmail.com", Email = "admin@gmail.com" }, Password = "admin1", Role = Roles.Admin },
                    new { User = new UserEntity { FirstName = "Boba", LastName = "Gray", UserName = "user@gmail.com", Email = "user@gmail.com" }, Password = "bobapass1", Role = Roles.User },
                    new { User = new UserEntity { FirstName = "Biba", LastName = "Undefined", UserName = "biba@gmail.com", Email = "biba@gmail.com" }, Password = "bibapass3", Role = Roles.User }
                };

                foreach (var i in users)
                {
                    var outcome = await _userManager.CreateAsync(i.User, i.Password);

                    if (!outcome.Succeeded)
                        Console.WriteLine($"Failed to create user: {i.User.UserName}");
                    else
                        outcome = await _userManager.AddToRoleAsync(i.User, i.Role);
                }
            }
        }

        public async Task SeedDataAsync()
        {
            await SeedCategoriesAsync();
            await SeedProductsAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
        }

    }
}