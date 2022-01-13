using AutoMapper;
using FluentAssertions;
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

namespace Rookie.AssetManagement.IntegrationTests.TestUsers
{
    public class TestUsersController_GetUsers : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private readonly UsersController _userController;
        public TestUsersController_GetUsers(SqliteInMemoryFixture fixture)
        {
            fixture.CreateDatabase();
            _dbContext = fixture.Context;
            _userRepository = new BaseRepository<User>(_dbContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            var storeStub = new Mock<IUserStore<User>>();
            var userManagerStub = new Mock<UserManager<User>>(storeStub.Object, null, null, null, null, null, null, null, null);
            userManagerStub.Setup(u => u.GetUsersInRoleAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<User>());
            _userManager = userManagerStub.Object;

            _userService = new UserService(_userManager, _userRepository, _mapper);
            _userController = new UsersController(_userManager, _userService);
            UsersData.InitUsersData(_dbContext);
        }

        [Fact]
        public async Task Get_Users_Success()
        {
            //Arrange
            var UserQueryCriteriaDto = UsersData.GetUserQueryCriteriaDto();

            // Act
            var result = await _userController.GetUsers(UserQueryCriteriaDto, new CancellationToken());

            // Assert
            result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);
            result.Should().NotBeNull();

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedResponseModel<UserDto>>(actionResult.Value);
            Assert.Equal(6, returnValue.TotalItems);
        }

        [Fact]
        public async Task Get_Users_WithSearch_ReturnExpectedUsers() 
		{
            // Arrange 
            var searchTerm = "Users 1";
            var userQueryCriteriaDto = UsersData.GetUserQueryCriteriaDtoWithSearch(searchTerm);

            // Act
            var result = await _userController.GetUsers(userQueryCriteriaDto, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedResponseModel<UserDto>>(actionResult.Value);
            Assert.All(returnValue.Items, i => i.FullName.Contains(searchTerm));
        }

        [Fact]
        public async Task Get_Users_WithUnexistedSearch_ReturnEmptyList()
		{
            // Arrange 
            var searchTerm = "Dummy";
            var userQueryCriteriaDto = UsersData.GetUserQueryCriteriaDtoWithSearch(searchTerm);

            // Act
            var result = await _userController.GetUsers(userQueryCriteriaDto, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedResponseModel<UserDto>>(actionResult.Value);
            Assert.Empty(returnValue.Items);
        }

        [Fact]
        public async Task Get_Users_WithPagination_ReturnPagedList()
        {
            // Arrange 
            int page = 2;
            int itemsPerPage = 3;
            var userQueryCriteriaDto = UsersData.GetUserQueryCriteriaDtoWithPagination(page, itemsPerPage);

            // Act
            var result = await _userController.GetUsers(userQueryCriteriaDto, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedResponseModel<UserDto>>(actionResult.Value);
            returnValue.Items.Should().HaveCount(itemsPerPage);
            Assert.Equal(page, returnValue.CurrentPage);
        }
    }
}