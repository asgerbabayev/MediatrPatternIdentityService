using Microsoft.Extensions.Logging;

namespace Code.Infrastructure.Persistance
{
    public class AppDbContextInitializer
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AppDbContextInitializer> _logger;

        public AppDbContextInitializer(AppDbContext appDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AppDbContextInitializer> logger)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task InitialAsync()
        {
            try
            {
                if (_appDbContext.Database.IsSqlServer())
                    await _appDbContext.Database.MigrateAsync();
            }
            catch (Exception)
            {
                _logger.LogError("An error occured while initialising the database...");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception)
            {
                _logger.LogError("An error occured while seeding the database...");
                throw;
            }
        }
        public async Task TrySeedAsync()
        {
            //Default Roles
            var adminRole = new IdentityRole("admin");
            if (!await _roleManager.RoleExistsAsync(adminRole.Name))
                await _roleManager.CreateAsync(adminRole);

            //Default Roles
            var userRole = new AppUser { UserName = "admin@localhost", Email = "admin@localhost.com" };
            if (_userManager.Users.All(u => u.UserName != userRole.UserName))
            {
                await _userManager.CreateAsync(userRole,"Pa$$word1");
                await _userManager.AddToRoleAsync(userRole, adminRole.Name);

                //Add Multiple Role\
                //await _userManager.AddToRolesAsync(userRole, new[] { adminRole.Name });
            }
            if (!_appDbContext.Categories.Any())
            {
                await _appDbContext.Categories.AddRangeAsync(new List<Category> {
                    new Category { Name = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" },
                    new Category { Name = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings" },
                    new Category { Name = "Confections", Description = "Desserts, candies, and sweet breads" },
                    new Category { Name = "Dairy Products", Description = "Cheeses" },
                    new Category { Name = "Grains Cereals", Description = "Breads, crackers, pasta, and cereal" },
                    new Category { Name = "Meat Poultry", Description = "Prepared meats" },
                    new Category { Name = "Produce", Description = "Dried fruit and bean curd" },
                    new Category { Name = "Seafood", Description = "Seaweed and fish" },
                });
            }
            await _appDbContext.SaveChangesAsync();
        }
    }
}
