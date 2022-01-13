using System;
using System.Threading.Tasks;
using Rookie.AssetManagement.Constants;
using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.DataAccessor.Entities;
using System.Linq;

namespace Rookie.AssetManagement.DataAccessor.Data.Seeds
{
    public static class DefaultUsers
    {
		public static async Task SeedAsync(UserManager<User> userManager)
		{
			if (!userManager.Users.Any())
			{
				var adminHCM = new User
				{
					UserName = "adminhcm",
                    Status = true
				};

				await userManager.CreateAsync(adminHCM, "123456");
			}

            if (!userManager.Users.Any())
            {
                var admin1 = new User
                {
                    UserName = "admin1",
                    Email = "admin1@gmail.com",
                    FullName = "Admin 1",
                    Status = true,
                    ChangePassword = false
                };
                await userManager.CreateAsync(admin1, "123456");
            }
            if (!userManager.Users.Any())
            {
                var admin2 = new User
                {
                    UserName = "admin2",
                    Email = "admin1@gmail.com",
                    FullName = "Admin 2",
                    Status = true,
                    ChangePassword = false
                };
                await userManager.CreateAsync(admin2, "123456");
            }

        }

		public static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("staff").Result == null)
            {
                User user = new User();
                user.UserName = "staff";
                user.Email = "user@gmail.com";
                user.Status = false;

                await userManager.CreateAsync(user, "123456");
            }


            if (userManager.FindByNameAsync("adminhn").Result == null)
            {
                User user = new User();
                user.UserName = "adminhn";
                user.Email = "adminhn@gmail.com";
                user.Status = true;

                await userManager.CreateAsync(user, "123456");
            }

            
        }
    }
}
