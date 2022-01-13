using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Constants;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.DataAccessor.Enums;

namespace Rookie.AssetManagement.IntegrationTests.TestData
{
    public static class UsersData
    {
        public static List<User> GetSeedUsersData()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    UserName = "testu1",
                    FullName = "Test User 1",
                    Status = true,
                },
                new User()
                {
                    Id = 2,
                    UserName = "testu2",
                    FullName = "Test User 2",
                    Status = true,
                },
                new User()
                {
                    Id = 3,
                    UserName = "testu3",
                    FullName = "Test User 3",
                    Status = true,
                },
                new User()
                {
                    Id = 4,
                    UserName = "testu4",
                    FullName = "Test User 4",
                    Status = true,
                },
                new User()
                {
                    Id = 5,
                    UserName = "testu5",
                    FullName = "Test User 5",
                    Status = true,
                },
                new User()
                {
                    Id = 6,
                    UserName = "testu6",
                    FullName = "Test User 6",
                    Status = true,
                }
            };
        }

        public static List<IdentityRole<int>> GetSeedRolesData()
		{
            return new List<IdentityRole<int>>()
            {
                new IdentityRole<int>()
                { 
                    Id = 1,
                    Name = Roles.Admin,
                    NormalizedName = Roles.Admin,
                    ConcurrencyStamp = "admin",
                },
                new IdentityRole<int>()
				{
                    Id = 2,
                    Name = Roles.Staff,
                    NormalizedName = Roles.Staff,
                    ConcurrencyStamp = "staff",
                },
            };
		}

        public static void InitUsersData(ApplicationDbContext dbContext)
        {
            var Users = GetSeedUsersData();
            dbContext.Users.AddRange(Users);
            dbContext.SaveChanges();
        }

        public static void InitRolesData(ApplicationDbContext dbContext)
		{
            var roles = GetSeedRolesData();
            dbContext.Roles.AddRange(roles);
            dbContext.SaveChanges();
        }

        public static UserQueryCriteriaDto GetUserQueryCriteriaDto()
        {
            return new UserQueryCriteriaDto()
            {
                Limit = 5,
                Page = 1,
                Location = null,
            };
        }

        public static UserQueryCriteriaDto GetUserQueryCriteriaDtoWithSearch(string search)
		{
            return new UserQueryCriteriaDto()
            {
                Limit = 5,
                Page = 1,
                Search = search,
            };
        }

        public static UserQueryCriteriaDto GetUserQueryCriteriaDtoWithPagination(int page, int itemsPerPage)
        {
            return new UserQueryCriteriaDto()
            {
                Limit = itemsPerPage,
                Page = page,
            };
        }
    }
}