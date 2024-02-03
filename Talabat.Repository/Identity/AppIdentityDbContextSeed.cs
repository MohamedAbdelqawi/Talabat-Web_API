using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "mohamednada",
                    Email = "mohamednada@gmail.com",
                    UserName = "mohamednada",
                    PhoneNumber = "01122334455",

                };
                try
                {
                    var reslut = await userManager.CreateAsync(user, "passW0rd!");
                }
                catch (Exception)
                {


                }

            }
        }
    }
}
