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
	public class TestUsersController_EditUser : IClassFixture<SqliteInMemoryFixture>
	{
        private readonly ApplicationDbContext _dbContext;
        private readonly BaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private readonly UsersController _userController;

        public TestUsersController_EditUser(SqliteInMemoryFixture fixture)
        {
            fixture.CreateDatabase();
            _dbContext = fixture.Context;
            _userRepository = new BaseRepository<User>(_dbContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            _userManager = fixture.UserManager;

            _userService = new UserService(_userManager, _userRepository, _mapper);
            _userController = new UsersController(_userManager, _userService);

            EditUserDto editUserDto = new()
            {
                DateOfBirth = new DateTime(2000, 2, 2),
                JoinedDate = new DateTime(2021, 11, 29),
                Gender = Genders.Male,
                Type = Roles.Staff,
            };

            //UsersData.InitUsersData(_dbContext);
            UsersData.InitRolesData(_dbContext);
        }

        [Fact]
        public async Task Edit_User_WithUnexistingUser_ReturnStatus404NotFound()
		{
            // Arrange 
            EditUserDto editUserDto = new()
            {
                DateOfBirth = new DateTime(2000, 2, 2),
                JoinedDate = new DateTime(2021, 11, 29),
                Gender = Genders.Male,
                Type = Roles.Staff,
            };

            // Act
            var result = await _userController.EditUser(1, editUserDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().HaveStatusCode(StatusCodes.Status404NotFound);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Edit_User_WithExistingUser_ReturnStatus200OK()
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
            EditUserDto editUserDto = new()
            {
                DateOfBirth = new DateTime(2000, 2, 2),
                JoinedDate = createUserDto.JoinedDate,
                Gender = createUserDto.Gender,
                Type = createUserDto.Type,
            };
            await _userController.CreateUser(createUserDto);

            // Act
            var result = await _userController.EditUser(1, editUserDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserDto>(actionResult.Value);
            Assert.Equal(1, returnValue.StaffCode);
            Assert.Equal(editUserDto.DateOfBirth, returnValue.DateOfBirth);
            Assert.NotNull(await _userManager.FindByNameAsync(returnValue.Username));
        }

        [Fact]
        public async Task Edit_User_WithExistingUser_ChangeUserRole_ReturnStatus200OK()
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
            EditUserDto editUserDto = new()
            {
                DateOfBirth = createUserDto.DateOfBirth,
                JoinedDate = createUserDto.JoinedDate,
                Gender = createUserDto.Gender,
                Type = Roles.Admin,
            };
            await _userController.CreateUser(createUserDto);

            // Act
            var result = await _userController.EditUser(1, editUserDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserDto>(actionResult.Value);
            Assert.Equal(1, returnValue.StaffCode);
            Assert.Equal(editUserDto.DateOfBirth, returnValue.DateOfBirth);

            var user = await _userManager.FindByNameAsync(returnValue.Username);
            Assert.NotNull(user);
            Assert.True(await _userManager.IsInRoleAsync(user, Roles.Admin));
        }
    }
}
