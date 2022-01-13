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

namespace Rookie.AssetManagement.IntegrationTests
{
    public class AuthenControllerShould : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly AuthenController _authenController;

        public AuthenControllerShould(SqliteInMemoryFixture fixture)
        {
            fixture.CreateDatabase();
            _dbContext = fixture.Context;
            _userRepository = new BaseRepository<User>(_dbContext);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            _userManager = fixture.UserManager;

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                 path: "appsettings.json",
                 optional: false,
                 reloadOnChange: true)
           .Build();

            _authenController = new AuthenController(_userManager, configuration);

            ArrangeData.InitUsersData(_dbContext, _userManager);
        }

        [Fact]
        public async Task Login_ValidAccount()
        {
            //Arrange
            var loginRequest = LoginRequestArrangeData.ValidAccount();

            // Act
            var result = await _authenController.Login(loginRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task Login_InvalidAccount()
        {
            //Arrange
            var loginRequest = LoginRequestArrangeData.InvalidAccount();

            // Act
            var result = await _authenController.Login(loginRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task Login_DisableAccount()
        {
            //Arrange
            var loginRequest = LoginRequestArrangeData.DisableAccount();

            // Act
            var result = await _authenController.Login(loginRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
