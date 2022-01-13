using Rookie.AssetManagement.Tests;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business;
using Rookie.AssetManagement.Business.Services;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Controllers;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.IntegrationTests.Common;
using Rookie.AssetManagement.IntegrationTests.TestData;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System;

namespace Rookie.AssetManagement.IntegrationTests.TestAsset
{
    public class TestAssetsController_CreateAsset : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BaseRepository<Asset> _assetRepository;
        private readonly BaseRepository<State> _stateRepository;
        private readonly BaseRepository<Category> _categoryRepository;

        private readonly IMapper _mapper;
        private readonly AssetService _assetService;
        private readonly AssetsController _assetController;

        public TestAssetsController_CreateAsset(SqliteInMemoryFixture fixture)
        {
            fixture.CreateDatabase();
            _dbContext = fixture.Context;
            _assetRepository = new BaseRepository<Asset>(_dbContext);
            _stateRepository = new BaseRepository<State>(_dbContext);
            _categoryRepository = new BaseRepository<Category>(_dbContext);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            _assetService = new AssetService(_assetRepository, _stateRepository, _categoryRepository, _mapper);
            _assetController = new AssetsController(_assetService);

            AssetData.InitStatesData(_dbContext);
            AssetData.InitCategoriesData(_dbContext);
            AssetData.InitAssetsData(_dbContext);
        }

        [Fact]
        public async Task Create_Assets_Success()
        {
            //Arrange
            CreateAssetDto createAssetDto = new()
            {
                AssetName = "Test",
                InstalledDate = new DateTime(2000, 1, 1),
                Specification = "hello",
                Location = "HCM",
                Category = "Laptop",
                State = "Available",
            };

            // Act
            var result = await _assetController.CreateAsset(createAssetDto);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().HaveStatusCode(StatusCodes.Status201Created);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<AssetDto>(actionResult.Value);
            Assert.Equal("LA100005", returnValue.AssetCode);
            Assert.Equal("Test", returnValue.AssetName);
        }
    }

}
