using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Constants;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;

namespace Rookie.AssetManagement.IntegrationTests.TestData
{
    public static class ArrangeData
    {
        public static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("user1").Result == null)
            {
                User user = new User();
                user.UserName = "user1";
                user.Email = "user1@gmail.com";
                user.Status = true;

                await userManager.CreateAsync(user, "123456");
            }
            if (userManager.FindByNameAsync("user2").Result == null)
            {
                User user = new User();
                user.UserName = "user2";
                user.Email = "user2@gmail.com";
                user.Status = false;

                await userManager.CreateAsync(user, "123457");
            }
            if (userManager.FindByNameAsync("user3").Result == null)
            {
                User user = new User();
                user.UserName = "user3";
                user.Email = "user3@gmail.com";
                user.Status = false;

                await userManager.CreateAsync(user, "123458");
            }
        }
        public static async void InitUsersData(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            await SeedUsersAsync(userManager);
            dbContext.SaveChanges();
        }
    }
}