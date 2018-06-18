using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using RPDControlSystem.Models.RPD;
using System;
using System.Linq;

namespace RPDControlSystem.Storage
{
    public static class Initializer
    {
        public static async Task InitializeRoleAsync(UserManager<TeacherProfile> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@rpd.com";
            string password = "P_Aa12_wd3456";

            string[] roles = new string[] {
                "admin",
                "teacher",
                "user"
            };

            foreach (var role in roles)
                if (await roleManager.FindByNameAsync(role) == null)
                    await roleManager.CreateAsync(new IdentityRole(role));

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                TeacherProfile admin = new TeacherProfile
                {
                    Email = adminEmail,
                    UserName = "administrator",
                    FirstName = "Admin",
                    LastName = "Admin",
                    MiddleName = "Admin",
                };
                IdentityResult result = null;
                try
                {
                    result = await userManager.CreateAsync(admin, password);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (result != null && result.Succeeded)
                    {

                        await userManager.AddToRoleAsync(admin, "admin");
                    }
                }



            }
        }
    }
}
