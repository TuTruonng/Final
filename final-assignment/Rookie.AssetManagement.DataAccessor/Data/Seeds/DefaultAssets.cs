using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.DataAccessor.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.DataAccessor.Data.Seeds
{
	public static class DefaultAssets
	{
		public static Asset[] SeedAssets()
		{
			int idx = 1;
			return new Asset[]
			{
				new()
				{
					AssetId = idx++,
					AssetName = "Laptop HP Pro Book 450 G1",
					CategoryId = 1,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 2,
					Specification = "Core i5, 8GB RAM, 750 GB HDD, Windows 8",
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Laptop HP Pro Book 450 G1",
					CategoryId = 1,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 2,
					Specification = "Core i5, 8GB RAM, 750 GB HDD, Windows 8",
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Laptop HP Pro Book 450 G1",
					CategoryId = 1,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 1,
					Specification = "Core i5, 8GB RAM, 750 GB HDD, Windows 8",
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Laptop HP Pro Book 450 G1",
					CategoryId = 1,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 3,
					Specification = "Core i5, 8GB RAM, 750 GB HDD, Windows 8",
					Location = "HCM",
				},
				// ------------------------------------------
				new()
				{
					AssetId = idx++,
					AssetName = "Monitor Dell UltraSharp",
					CategoryId = 2,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 2,
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Monitor Dell UltraSharp",
					CategoryId = 2,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 2,
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Monitor Dell UltraSharp",
					CategoryId = 2,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 1,
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Monitor Dell UltraSharp",
					CategoryId = 2,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 1,
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Monitor Dell UltraSharp",
					CategoryId = 2,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 2,
					Location = "HCM",
				},
				// ------------------------------------------
				new()
				{
					AssetId = idx++,
					AssetName = "Personal Computer",
					CategoryId = 3,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 2,
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Personal Computer",
					CategoryId = 3,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 2,
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Personal Computer",
					CategoryId = 3,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 3,
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Personal Computer",
					CategoryId = 3,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 1,
					Location = "HCM",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Personal Computer",
					CategoryId = 3,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 1,
					Location = "HCM",
				},
				// ------------------------------------------
				new()
				{
					AssetId = idx++,
					AssetName = "Laptop HP Pro Book 450 G1",
					CategoryId = 1,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 2,
					Location = "HN",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Monitor Dell UltraSharp",
					CategoryId = 2,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 3,
					Location = "HN",
				},
				new()
				{
					AssetId = idx++,
					AssetName = "Personal Computer",
					CategoryId = 3,
					InstalledDate = new DateTime(2021, 12, 8),
					StateId = 1,
					Location = "HN",
				},
			};
		}
	}
}
