using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.IntegrationTests.TestData
{
	static class AssetData
	{
		public static List<Asset> GetSeedAssetData()
		{
			return new List<Asset>()
			{
				new Asset()
				{
					AssetId = 1,
					AssetName = "Laptop HP Pro Book 450 G1",
					CategoryId = 1,
					InstalledDate = DateTime.Now,
					StateId = 2,
					Specification = "Core i5, 8GB RAM, 750 GB HDD, Windows 8",
					Location = "HCM",
				},
				new Asset()
				{
					AssetId = 2,
					AssetName = "Laptop HP Pro Book 450 G1",
					CategoryId = 1,
					InstalledDate = DateTime.Now,
					StateId = 2,
					Specification = "Core i5, 8GB RAM, 750 GB HDD, Windows 8",
					Location = "HCM",
				},
				new Asset()
				{
					AssetId = 3,
					AssetName = "Laptop HP Pro Book 450 G1",
					CategoryId = 2,
					InstalledDate = DateTime.Now,
					StateId = 1,
					Specification = "Core i5, 8GB RAM, 750 GB HDD, Windows 8",
					Location = "HCM",
				},
				new Asset()
				{
					AssetId = 4,
					AssetName = "Laptop HP Pro Book 450 G1",
					CategoryId = 2,
					InstalledDate = DateTime.Now,
					StateId = 2,
					Specification = "Core i5, 8GB RAM, 750 GB HDD, Windows 8",
					Location = "HCM",
				},
			};
		}

		public static List<State> GetSeedStateData()
		{
			return new List<State>()
			{
				new State()
				{
					StateId = 1,
					Name = "Assigned",
				},
				new State()
				{
					StateId = 2,
					Name = "Available"
				},

			};
		}

		public static List<Category> GetSeedCategoryData()
		{
			return new List<Category>()
			{
				new Category()
				{
					CategoryId = 1,
					Name = "Laptop",
					Prefix = "LA",
				},
				new Category()
				{
					CategoryId = 2,
					Name = "Monitor",
					Prefix = "MO",
				},
			};
		}

		public static void InitAssetsData(ApplicationDbContext dbContext)
		{
			var assets = GetSeedAssetData();
			dbContext.Assets.AddRange(assets);
			dbContext.SaveChanges();
		}

		public static void InitStatesData(ApplicationDbContext dbContext)
		{
			var states = GetSeedStateData();
			dbContext.States.AddRange(states);
			dbContext.SaveChanges();
		}

		public static void InitCategoriesData(ApplicationDbContext dbContext)
		{
			var categories = GetSeedCategoryData();
			dbContext.Categories.AddRange(categories);
			dbContext.SaveChanges();
		}

		public static AssetQueryCriteriaDto GetAssetQueryCriteriaDto()
		{
			return new AssetQueryCriteriaDto()
			{
				Limit = 5,
				Page = 1,
				Location = null,
			};
		}

		public static AssetQueryCriteriaDto GetAssetQueryCriteriaDtoWithSearch(string search)
		{
			return new AssetQueryCriteriaDto()
			{
				Limit = 5,
				Page = 1,
				Search = search,
			};
		}

		public static AssetQueryCriteriaDto GetAssetQueryCriteriaDtoWithPagination(int page, int itemsPerPage)
		{
			return new AssetQueryCriteriaDto()
			{
				Limit = itemsPerPage,
				Page = page,
			};
		}
	}
}
