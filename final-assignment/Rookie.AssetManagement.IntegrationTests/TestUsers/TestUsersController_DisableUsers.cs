using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using Rookie.AssetManagement.IntegrationTests.TestData;
using Rookie.AssetManagement.Tests;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Contracts.Constants;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Rookie.AssetManagement.Controllers;
using System.IO;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;

namespace Rookie.AssetManagement.IntegrationTests.TestUsers
{
    public class TestUsersController_DisableUsers : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private readonly UsersController _userController;
        public TestUsersController_DisableUsers(SqliteInMemoryFixture fixture)
        {
            fixture.CreateDatabase();
            _dbContext = fixture.Context;
            _userRepository = new BaseRepository<User>(_dbContext);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            _userManager = fixture.UserManager;

            _userService = new UserService(_userManager, _userRepository, _mapper);
            _userController = new UsersController(_userManager, _userService);

            UsersData.InitUsersData(_dbContext);
        }
        [Fact]
        public async Task Login_DisableAccount()
        {
            //Arrange
            var userID = 1;

            // Act
            var result = await _userController.DisableUser(userID);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserDto>(actionResult.Value);
            Assert.False(returnValue.Status);
        }
    }
}
