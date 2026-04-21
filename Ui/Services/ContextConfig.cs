using DAL.DbContext;
using DAL.UserModel;
using Microsoft.AspNetCore.Identity;

namespace Ui.Services
{
    public class ContextConfig
    {
        private readonly static string seedAdminEmail = "admin@gmail.com";
        private readonly static string seedUserEmail = "user@gmail.com";
        public static async Task SeedDataAsync(ShippingContext shippingContext, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            await SeedUserAcync(roleManager, userManager);
        }
        private static async Task SeedUserAcync(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new AppRole{ Name = "Admin" });

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new AppRole{ Name = "User" });

            var adminUser = await userManager.FindByEmailAsync(seedAdminEmail);

            if(adminUser == null)
            {
                Guid id = Guid.NewGuid();
                adminUser = new AppUser
                {
                    Id = id,
                    UserName = seedAdminEmail,
                    Email = seedAdminEmail,
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(adminUser, "admin123");
                if(result.Succeeded) 
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            var user = await userManager.FindByEmailAsync(seedUserEmail);
            if(user == null)
            {
                Guid id = Guid.NewGuid();
                user = new AppUser
                {
                    Id = id,
                    UserName = seedUserEmail,
                    Email = seedUserEmail,
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, "user123");
                if(result.Succeeded) 
                    await userManager.AddToRoleAsync(user, "User");
            }

        }
    }
}
