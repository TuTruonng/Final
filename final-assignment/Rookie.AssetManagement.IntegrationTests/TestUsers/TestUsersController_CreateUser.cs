using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos;
using Rookie.AssetManagement.Controllers;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using Rookie.AssetManagement.IntegrationTests.TestData;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.Tests;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using System.Collections.Generic;
using Rookie.AssetManagement.Contracts.Constants;
using Microsoft.AspNetCore.Identity;
using Moq;
using Rookie.AssetManagement.Constants;
using FluentAssertions;

namespace Rookie.AssetManagement.IntegrationTests.TestUsers
{
	public class TestUsersController_CreateUser : IClassFixture<SqliteInMemoryFixture>
	{
        private readonly ApplicationDbContext _dbContext;
        private readonly BaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private readonly UsersController _userController;

        public TestUsersController_CreateUser(SqliteInMemoryFixture fixture)
		{
            fixture.CreateDatabase();
            _dbContext = fixture.Context;
            _userRepository = new BaseRepository<User>(_dbContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            _userManager = fixture.UserManager;

            _userService = new UserService(_userManager, _userRepository, _mapper);
            _userController = new UsersController(_userManager, _userService);

            //UsersData.InitUsersData(_dbContext);
            UsersData.InitRolesData(_dbContext);
        }

        [Fact]
        public async Task Create_User_ReturnCreatedUser()
		{
            // Arrange 
            CreateUserDto createUserDto = new()
            {
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(2000, 1, 1),
                JoinedDate = new DateTime(2021, 11, 29),
                Gender = Genders.Male,
                Location = "HCM",
                Type = Roles.Staff,
            };

            // Act
            var result = await _userController.CreateUser(createUserDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().HaveStatusCode(StatusCodes.Status201Created);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<UserDto>(actionResult.Value);
            Assert.Equal(1, returnValue.StaffCode);
            Assert.Equal("testu", returnValue.Username);
            Assert.NotNull(_userManager.FindByNameAsync(returnValue.Username));
        }

        [Fact]
        public async Task Create_Users_ReturnCreatedUsersWithDifferentUsername()
        {
            // Arrange 
            CreateUserDto createUserDto = new()
            {
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(2000, 1, 1),
                JoinedDate = new DateTime(2021, 11, 29),
                Gender = Genders.Male,
                Location = "HCM",
                Type = Roles.Staff,
            };

            // Act
            var result1 = await _userController.CreateUser(createUserDto);
            var result2 = await _userController.CreateUser(createUserDto);

            // Assert
            result1.Should().NotBeNull();
            result1.Result.Should().HaveStatusCode(StatusCodes.Status201Created);

            var actionResult1 = Assert.IsType<CreatedAtActionResult>(result1.Result);
            var returnValue1 = Assert.IsType<UserDto>(actionResult1.Value);
            Assert.Equal(1, returnValue1.StaffCode);
            Assert.Equal("testu", returnValue1.Username);


            result2.Should().NotBeNull();
            result2.Result.Should().HaveStatusCode(StatusCodes.Status201Created);

            var actionResult2 = Assert.IsType<CreatedAtActionResult>(result2.Result);
            var returnValue2 = Assert.IsType<UserDto>(actionResult2.Value);
            Assert.Equal(2, returnValue2.StaffCode);
            Assert.Equal("testu1", returnValue2.Username);
        }
    }
}
