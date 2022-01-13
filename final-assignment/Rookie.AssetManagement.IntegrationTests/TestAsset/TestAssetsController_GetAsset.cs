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

namespace Rookie.AssetManagement.IntegrationTests.TestAsset
{
	public class TestAssetsController_GetAsset : IClassFixture<SqliteInMemoryFixture>
	{
        private readonly ApplicationDbContext _dbContext;
        private readonly BaseRepository<Asset> _assetRepository;
        private readonly BaseRepository<State> _stateRepository;
        private readonly BaseRepository<Category> _categoryRepository;

        private readonly IMapper _mapper;
        private readonly AssetService _assetService;
        private readonly AssetsController _assetController;

        public TestAssetsController_GetAsset(SqliteInMemoryFixture fixture)
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
		public void Get_Assets_Success()
		{
			//Arrange
			var AssetQueryCriteriaDto = AssetData.GetAssetQueryCriteriaDto();

			// Act
			var result = _assetController.GetAssets(AssetQueryCriteriaDto, new CancellationToken());

			// Assert
			result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);
			result.Should().NotBeNull();

			var actionResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnValue = Assert.IsType<PagedResponseModel<AssetDto>>(actionResult.Value);
			Assert.Equal(4, returnValue.TotalItems);
		}

		[Fact]
		public void Get_Asset_WithSearch_ReturnExpectedUsers()
		{
			// Arrange 
			var searchTerm = "Laptop HP Pro Book 450 G1";
			var AssetQueryCriteriaDto = AssetData.GetAssetQueryCriteriaDtoWithSearch(searchTerm);

			// Act
			var result = _assetController.GetAssets(AssetQueryCriteriaDto, new CancellationToken());

			// Assert
			result.Should().NotBeNull();
			result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);

			var actionResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnValue = Assert.IsType<PagedResponseModel<AssetDto>>(actionResult.Value);
			Assert.All(returnValue.Items, i => i.AssetName.Contains(searchTerm));
		}

		[Fact]
		public void Get_Asset_WithUnexistedSearch_ReturnEmptyList()
		{
			// Arrange 
			var searchTerm = "Dummy";
			var AssetQueryCriteriaDto = AssetData.GetAssetQueryCriteriaDtoWithSearch(searchTerm);

			// Act
			var result = _assetController.GetAssets(AssetQueryCriteriaDto, new CancellationToken());

			// Assert
			result.Should().NotBeNull();
			result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);

			var actionResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnValue = Assert.IsType<PagedResponseModel<AssetDto>>(actionResult.Value);
			Assert.Empty(returnValue.Items);
		}

		[Fact]
		public void Get_Asset_WithPagination_ReturnPagedList()
		{
			// Arrange 
			int page = 2;
			int itemsPerPage = 2;
			var AssetQueryCriteriaDto = AssetData.GetAssetQueryCriteriaDtoWithPagination(page, itemsPerPage);

			// Act
			var result = _assetController.GetAssets(AssetQueryCriteriaDto, new CancellationToken());

			// Assert
			result.Should().NotBeNull();
			result.Result.Should().HaveStatusCode(StatusCodes.Status200OK);

			var actionResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnValue = Assert.IsType<PagedResponseModel<AssetDto>>(actionResult.Value);
			returnValue.Items.Should().HaveCount(itemsPerPage);
			Assert.Equal(page, returnValue.CurrentPage);
		}
	}

}
